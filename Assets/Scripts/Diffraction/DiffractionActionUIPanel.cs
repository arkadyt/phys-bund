using Action;
using UnityEngine.UI;

namespace Diffraction {
    public class DiffractionActionUIPanel : ActionUIPanel {
        public Slider slLength, slFreq;
        public Text txtLength, txtFreq;
        public DiffractionController m_DiffractionController;

        public void OnLengthValueChanged() {
            m_DiffractionController.OnLengthValueChanged();
        }
        public void OnFrequencyValueChanged () {
            m_DiffractionController.OnFrequencyValueChanged();
        }
    }
}