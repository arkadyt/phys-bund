using PlayerControllers;
using System.Collections;
using UnityEngine;
namespace Action {
    public abstract class ActionObject : MonoBehaviour {
        [SerializeField] protected ActionCamera m_ActionCamera;
        [SerializeField] protected ActionUIPanel m_ActionUIPanel;
        [SerializeField] ActorController m_ActorController;
        [HideInInspector] public bool isLerping, isEntering = false;

        Camera actorCamera;
        Camera actionCamera;
        Vector3 camDefPos, startPos, targetPos;
        Quaternion camDefRot, startRot, targetRot;
        GameObject actorObject;

        public virtual void Start() {
            camDefPos = m_ActionCamera.transform.position;
            camDefRot = m_ActionCamera.transform.rotation;

            actionCamera = m_ActionCamera.GetComponent<Camera>();
            actorCamera = m_ActorController.GetComponentInChildren<Camera>();
        }
        void OnTriggerEnter(Collider c) {
            OnTrigger(c, true);
        }
        void OnTriggerExit(Collider c) {
            OnTrigger(c, false);
        }
        private void OnTrigger(Collider c, bool enter) {
            if (c.tag == "Actor") {
                m_ActorController.m_ActionController.m_ActionObject = enter ? this : null;
                actorObject = enter ? c.gameObject : null;
                m_ActorController.m_ActionController.m_ActorActionPanels.m_PressEPanel.SetActive(enter);
                m_ActorController.m_ActionController.m_ActorActionPanels.isEactive = enter;
            }
        }
        public virtual void enterAction() {
            startPos = actorCamera.transform.position;
            startRot = actorCamera.transform.rotation;
            targetPos = camDefPos;
            targetRot = camDefRot;
            switchCamera(true);
            isEntering = true;
            m_ActorController.m_ActionController.isInAction = true;
            m_ActorController.iCtrl.showCursor(true);
            setActionCamera(actorCamera.transform.position, actorCamera.transform.rotation);
            StartCoroutine(LerpBetweenCameras());
        }
        public virtual void exitAction() {
            startPos = actionCamera.transform.position;
            startRot = actionCamera.transform.rotation;
            targetPos = actorCamera.transform.position;
            targetRot = actorCamera.transform.rotation;
            activatePanels(false);
            isEntering = false;
            m_ActorController.m_ActionController.isInAction = false;
            m_ActorController.iCtrl.showCursor(false);
            StartCoroutine(LerpBetweenCameras());
        }
        IEnumerator LerpBetweenCameras () {
            isLerping = true;
            float i = 0.0f;
            float rate = 1 / m_ActorController.m_ActionController.m_SmoothTime;
            float startFov = actionCamera.fieldOfView;
            while (i < 1.0) {
                i += Time.deltaTime * rate;
                actionCamera.transform.position = Vector3.Lerp(startPos, targetPos, i);
                actionCamera.transform.rotation = Quaternion.Lerp(startRot, targetRot, i);
                actionCamera.fieldOfView = startFov + (actorCamera.fieldOfView-startFov)*i;
                if (i >= 1.0f) {
                    actionCamera.fieldOfView = actorCamera.fieldOfView;
                    lerpingFinished();
                }
                yield return null;
            }
        }
        void lerpingFinished() {
            setActionCamera(targetPos, targetRot);

            isLerping = false;
            if (isEntering) {
                activatePanels(true);
            }
            else {
                setActionCamera(camDefPos, camDefRot);
                switchCamera(false);
            }
        }
        void setActionCamera(Vector3 position, Quaternion rotation) {
            actionCamera.transform.position = position;
            actionCamera.transform.rotation = rotation;
        }
        void activatePanels(bool enter) {
            m_ActionUIPanel.gameObject.SetActive(enter);
            m_ActorController.m_ActionController.m_ActorActionPanels.m_CloseActionPanel.SetActive(enter);
        }
        // Turns on/off Actor and switches off/on ActionCamera
        void switchCamera(bool enter) {
            m_ActorController.m_ActionController.m_ActorActionPanels.openEPanel(!enter);
            actionCamera.gameObject.SetActive(enter);
            actorObject.SetActive(!enter);
        }
    }
}