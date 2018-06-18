using Action;
using Pendulum;
using UnityEngine;

namespace Tanks {
    public class TanksActionObject : ActionObject {
        [SerializeField] TanksController m_TanksController;
        public override void enterAction() {
            base.enterAction();
            m_TanksController.canShoot = true;
        }
        public override void exitAction() {
            base.exitAction();
            m_TanksController.canShoot = false;
            m_ActionUIPanel.sliderFOV.value = m_ActionUIPanel.sliderFOV.minValue;
            m_ActionCamera.OnMouseFovChanged();
        }

    }
}