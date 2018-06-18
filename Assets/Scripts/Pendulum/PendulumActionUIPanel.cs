using Action;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Pendulum {
    public class PendulumActionUIPanel : ActionUIPanel {
        public Slider slLength, slGravity, slTime, slAngle;
        public Text txtLength, txtGravity, txtTime, txtAngle;
        public Toggle toggleTimeFreeze, toggleAccVectors, toggleForceVectors;
        public PendulumController pendulum;

        private PendulumActionCamera m_PendulumActionCamera;

        void Start(){
            m_PendulumActionCamera = (PendulumActionCamera)m_ActionCamera;
        }

        public void OnLengthChanged () {
            pendulum.OnLengthChanged();
        }
        public void OnGravityChanged () {
            pendulum.OnGravityChanged();
        }
        public void OnTimeChanged () {
            pendulum.OnTimeChanged();
        }
        public void OnAngleChanged () {
            pendulum.OnAngleChanged();
        }
        public void OnToggleTimeFreeze() {
            pendulum.OnTimeFreeze();
        }
        public void OnToggleAccVectors() {
            m_PendulumActionCamera.OnToggleAccVectors();
        }
        public void OnToggleForceVectors() {
            m_PendulumActionCamera.OnToggleForceVectors();
        }
    }
}