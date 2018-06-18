using Action;

namespace Diffraction {
    public class DiffractionActionObject : ActionObject{
        public override void exitAction() {
            m_ActionUIPanel.sliderFOV.value = m_ActionUIPanel.sliderFOV.minValue;
            m_ActionCamera.OnMouseFovChanged();
            base.exitAction();
        }
    }
}