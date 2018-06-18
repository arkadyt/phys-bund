using Action;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Pendulum {
    public class PendulumActionObject : ActionObject {
        PendulumActionUIPanel m_PendulumActionUIPanel;

        public override void Start() {
            base.Start();
            m_PendulumActionUIPanel = (PendulumActionUIPanel) m_ActionUIPanel;
        }
        public override void exitAction() {
            m_PendulumActionUIPanel.toggleLockView.isOn =
            //m_PendulumActionUIPanel.toggleForceVectors.isOn =
            //m_PendulumActionUIPanel.toggleAccVectors.isOn =
            m_PendulumActionUIPanel.toggleTimeFreeze.isOn = false;
            m_PendulumActionUIPanel.slTime.value = 1;
            m_PendulumActionUIPanel.sliderFOV.value = m_PendulumActionUIPanel.sliderFOV.minValue;

            m_PendulumActionUIPanel.OnToggleLockView();
            //m_PendulumActionUIPanel.OnToggleAccVectors();
            //m_PendulumActionUIPanel.OnToggleAccVectors();
            m_PendulumActionUIPanel.OnToggleTimeFreeze();
            m_PendulumActionUIPanel.OnTimeChanged();
            m_ActionCamera.OnMouseFovChanged();
            base.exitAction();
        }
    }
}
