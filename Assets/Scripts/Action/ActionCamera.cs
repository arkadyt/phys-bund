using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Action {
    public abstract class ActionCamera : MonoBehaviour {
        public ActionObject m_ActionObject;
        public ActionUIPanel m_ActionUIPanel;
        public Transform m_Target;
        public float xSensitivity = 5.0f;
        public float ySensitivity = 2.4f;
        public float scrollSpeed = 20.0f;
        public float yMinLimit = -20.0f;
        public float yMaxLimit = 20.0f;
        public float xMinLimit = -30.0f;
        public float xMaxLimit = 30.0f;
        public float fovZoomFactor = 0.05f;

        protected bool lockTargetView = false;
        private float x;
        private float y;

        void LateUpdate () {
            if (m_ActionObject.isLerping) return;

            OnMouseFovChanged();

            if (lockTargetView && m_Target != null) {
                transform.LookAt(m_Target.transform);
            } else {
                if (!Input.GetMouseButton(1)) return;

                float zoomFactor = ((1 - fovZoomFactor) / (m_ActionUIPanel.sliderFOV.maxValue - m_ActionUIPanel.sliderFOV.minValue)) * (GetComponent<Camera>().fieldOfView - m_ActionUIPanel.sliderFOV.minValue) + fovZoomFactor;
                x = Input.GetAxis("Mouse X") * xSensitivity * zoomFactor;
                y = -Input.GetAxis("Mouse Y") * ySensitivity * zoomFactor;

                transform.localRotation *= Quaternion.Euler(y, x, 0);
                x = transform.localRotation.eulerAngles.x;
                y = transform.localRotation.eulerAngles.y;

                x = ClampAngle(x, xMinLimit, xMaxLimit);
                y = ClampAngle(y, yMinLimit, yMaxLimit);
                transform.localRotation = Quaternion.Euler(x, y, 0);
            }
        }

        private float ClampAngle (float angle, float min, float max) {
            if (angle > 180 && angle < 360) {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
        public void OnToggleLockView() {
            lockTargetView = m_ActionUIPanel.toggleLockView.isOn;
        }
        public void OnMouseFovChanged() {
            if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
            m_ActionUIPanel.sliderFOV.value += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            GetComponent<Camera>().fieldOfView = 60 - m_ActionUIPanel.sliderFOV.value;
        }
    }
}