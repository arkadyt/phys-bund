using UnityEngine;
using UnityEngine.UI;
namespace Action {
    public abstract class ActionUIPanel : MonoBehaviour {
        public ActionCamera m_ActionCamera;
        public Slider sliderFOV;
        public Toggle toggleLockView;

        public void OnToggleLockView() {
            m_ActionCamera.OnToggleLockView();
        }
        public void OnUIFovChanged(){
            m_ActionCamera.GetComponent<Camera>().fieldOfView = 60 - sliderFOV.value;
        }
    }
}