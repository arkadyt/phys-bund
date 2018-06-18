using UnityEngine;
using System.Collections;
namespace Diffraction {
    public class DiffractionController : MonoBehaviour {
        [SerializeField] DiffractionActionObject m_DiffractionActionObject;
        [SerializeField] DiffractionActionUIPanel m_DiffractionActionUIPanel;
        [SerializeField] Transform m_DiffractionPlane;
        [SerializeField] GameObject[]colorObjects;
        private float planeScaleXDefault;
        private const float c = 3 * 100000;

        void Start() {
            planeScaleXDefault = m_DiffractionPlane.localScale.x;
        }

        public void OnLengthValueChanged () {
            m_DiffractionActionUIPanel.txtLength.text = f("Wave Length: {0} nm", m_DiffractionActionUIPanel.slLength.value);
            m_DiffractionActionUIPanel.slFreq.value = c / m_DiffractionActionUIPanel.slLength.value;
            m_DiffractionActionUIPanel.txtFreq.text = f("Wave Frequency: {0} THz", m_DiffractionActionUIPanel.slFreq.value);
            updateColor();
        }
        public void OnFrequencyValueChanged () {
            m_DiffractionActionUIPanel.txtFreq.text = f("Wave Frequency: {0} THz", m_DiffractionActionUIPanel.slFreq.value);
            m_DiffractionActionUIPanel.slLength.value = c / m_DiffractionActionUIPanel.slFreq.value;
            m_DiffractionActionUIPanel.txtLength.text = f("Wave Length: {0} nm", m_DiffractionActionUIPanel.slLength.value);
            updateColor();
        }
        private void updateColor () {
            // Change the color
            float frequencyProgress = (m_DiffractionActionUIPanel.slFreq.value - m_DiffractionActionUIPanel.slFreq.minValue) /
            (m_DiffractionActionUIPanel.slFreq.maxValue - m_DiffractionActionUIPanel.slFreq.minValue);
            Color color = HSVToRGB(frequencyProgress * 280 / 360, 1, 1);
            //Color af=Color.HSVToRGB(frequencyProgress * 280 / 360, 1, 1);
            color.a = 0.95f;
            foreach (GameObject obj in colorObjects) {
                Renderer rend = obj.GetComponent<Renderer>();
                rend.material.SetColor("_Color", color);
            }
            //Scale the plane
            Vector3 scale = m_DiffractionPlane.localScale;
            scale.x = planeScaleXDefault / 2 * (2 - frequencyProgress);
            m_DiffractionPlane.localScale = scale;
        }

        private string f (string format, params object[]args) {
            return string.Format(format, args);
        }
        private static Color HSVToRGB (float H, float S, float V) {
            Color white = Color.white;
            if (S == 0f)
            {
                white.r = V;
                white.g = V;
                white.b = V;
            }
            else
            {
                if (V == 0f)
                {
                    white.r = 0f;
                    white.g = 0f;
                    white.b = 0f;
                }
                else
                {
                    white.r = 0f;
                    white.g = 0f;
                    white.b = 0f;
                    float num = H * 6f;
                    int num2 = (int)Mathf.Floor(num);
                    float num3 = num - (float)num2;
                    float num4 = V * (1f - S);
                    float num5 = V * (1f - S * num3);
                    float num6 = V * (1f - S * (1f - num3));
                    int num7 = num2;
                    switch (num7 + 1) {
                        case 0:
                        white.r = V;
                        white.g = num4;
                        white.b = num5;
                        break;
                        case 1:
                        white.r = V;
                        white.g = num6;
                        white.b = num4;
                        break;
                        case 2:
                        white.r = num5;
                        white.g = V;
                        white.b = num4;
                        break;
                        case 3:
                        white.r = num4;
                        white.g = V;
                        white.b = num6;
                        break;
                        case 4:
                        white.r = num4;
                        white.g = num5;
                        white.b = V;
                        break;
                        case 5:
                        white.r = num6;
                        white.g = num4;
                        white.b = V;
                        break;
                        case 6:
                        white.r = V;
                        white.g = num4;
                        white.b = num5;
                        break;
                        case 7:
                        white.r = V;
                        white.g = num6;
                        white.b = num4;
                        break;
                    }
                }
            }
            return white;
        }
    }
}
