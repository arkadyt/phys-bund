using Action;
using Pendulum;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Pendulum {
    public class PendulumActionCamera : ActionCamera {
        public GameObject m_ForceVectors, m_AccVectors;
        private PendulumActionUIPanel m_PendulumActionUIPanel;

        void Start() {
            m_PendulumActionUIPanel = (PendulumActionUIPanel)m_ActionUIPanel;
        }

        public void OnToggleAccVectors() {
            m_AccVectors.SetActive(m_PendulumActionUIPanel.toggleAccVectors.isOn);
        }
        public void OnToggleForceVectors() {
            m_ForceVectors.SetActive(m_PendulumActionUIPanel.toggleForceVectors.isOn);
        }
    }
}