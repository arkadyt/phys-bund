using Action;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks {
    public class TanksActionUIPanel : ActionUIPanel {
        public Slider slAngle;
        public Slider slSpeed;
        public Text txtAngle;
        public Text txtSpeed;
        public TanksController m_TanksController;

        public void OnAngleChanged() {
            m_TanksController.OnAngleChanged();
        }
        public void OnSpeedChanged() {
            m_TanksController.OnSpeedChanged();
        }
       /* public void OnRestart() {
            m_TanksController.OnRestart();
        }*/
    }
}