using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Pendulum {
    public class PendulumController : MonoBehaviour {
        [SerializeField] PendulumActionUIPanel m_PendulumUIActionPanel;
        [SerializeField] float vectorLength = 3;
        public Transform
        pendulum, ball, thread,
        vectorAcc,  vectorAccCenter,  vectorAccTangent,
        vectorMg, vectorF, vectorN;
        private float
        angleAmplitude,
        accVectorAmp,
        startTime,
        period,
        l,
        g;

        void Start () {
            updateValuesAndReset();
        }

        Transform vectorChild(Transform vectorFull, bool cone) {
            return vectorFull.FindChild(cone? "Cone" : "Cylinder");
        }
        void Update () {
            float t = Time.time - startTime;
            float z = angleAmplitude * Mathf.Cos(2 * Mathf.PI * t / period);
            pendulum.localRotation = Quaternion.Euler(pendulum.localRotation.x, pendulum.localRotation.y, z);

            scaleVector(vectorChild(vectorAccCenter, false), vectorChild(vectorAccCenter, true), t, accVectorAmp, true, false);
            scaleVector(vectorChild(vectorAccTangent, false), vectorChild(vectorAccTangent, true), t, accVectorAmp, false, true);
            scaleVectorCustom(vectorChild(vectorN, false), vectorChild(vectorN, true), getNscale(t));

            rotateVector(vectorAcc, t);
            rotateVector(vectorF, t);
            rotateVectorCustom(vectorMg, 180);
        }
	#region SliderControllers
    public void OnLengthChanged () {
        m_PendulumUIActionPanel.txtLength.text = "Thread Length: " + formatVal(m_PendulumUIActionPanel.slLength.value, 1);
        updateValuesAndReset();

        thread.localScale = new Vector3(thread.localScale.x, l, thread.localScale.z);
        ball.localPosition = new Vector3(ball.localPosition.x, -l * 2, ball.localPosition.z);
    }

        public void OnGravityChanged () {
            m_PendulumUIActionPanel.txtGravity.text = "Free Fall Acceleration: " + formatVal(m_PendulumUIActionPanel.slGravity.value * 10, 3);
            updateValuesAndReset();
        }

        public void OnTimeChanged () {
            Time.timeScale = m_PendulumUIActionPanel.slTime.value;
            m_PendulumUIActionPanel.txtTime.text = "Time: x" + formatVal(m_PendulumUIActionPanel.slTime.value, 3);
        }
        public void OnTimeFreeze() {
            Time.timeScale = m_PendulumUIActionPanel.toggleTimeFreeze.isOn ? 0 : m_PendulumUIActionPanel.slTime.value;
            m_PendulumUIActionPanel.slTime.interactable = !m_PendulumUIActionPanel.toggleTimeFreeze.isOn;
        }
        public void OnAngleChanged () {
            m_PendulumUIActionPanel.txtAngle.text = "Angle: " + (m_PendulumUIActionPanel.slAngle.value);
            updateValuesAndReset();
        }
	#endregion
    private string formatVal (float value, int depth) {
        string str = value.ToString();
        string zeros = "0";
        for (int i = 1; i < depth; i++) {
            zeros = zeros + "0";
        }
        return str.Length > 2 + depth ? str.Substring(0, 2 + depth) : (str.Length == 1 ? str + "." + zeros : str);
    }
        private void scaleVector (Transform cylinder, Transform cone, float time, float amplitude, bool Sine, bool switchToNegative) {
            float val = 2 * Mathf.PI * time / period;
            float scaleY = Sine ? Mathf.Sin(val) : Mathf.Cos(val);
            scaleY = Mathf.Abs(amplitude * scaleY);
            bool negative = switchToNegative && pendulum.localRotation.z > 0;
            int sign = (negative) ? 1 : -1;
            cylinder.localScale = new Vector3(cylinder.localScale.x, scaleY, cylinder.localScale.z);
            cone.localPosition = new Vector3(cone.localPosition.x, vectorLength * sign*(1-scaleY), cone.localPosition.z);

            float angleZ = negative ? 180 : 0;
            cylinder.localRotation = Quaternion.Euler(0, 0, angleZ);
            cone.localRotation = Quaternion.Euler(0, 0, angleZ);
        }
        /**
        private void scaleVector (Transform cylinder, Transform cone, float time, float amplitude, bool Sine, bool NegatieVal) {
            float val = 2 * Mathf.PI * time / period;
            float scaleY = Sine ? Mathf.Sin(val) : Mathf.Cos(val);
            scaleY = Mathf.Abs(amplitude * scaleY);
            bool negative = NegatieVal && pendulum.localRotation.z > 0;
            cylinder.localScale = new Vector3(cylinder.localScale.x, scaleY, cylinder.localScale.z);
            cone.localPosition = new Vector3(cone.localPosition.x, negative ? vectorLength * (1 - scaleY) : vectorLength * (scaleY - 1), cone.localPosition.z);

            cylinder.localRotation = Quaternion.Euler(0, 0, negative ? 180 : 0);
            cone.localRotation = Quaternion.Euler(0, 0, negative ? 180 : 0);
        }*/
        private void scaleVectorCustom (Transform cylinder, Transform cone, float scaleY) {
            cylinder.localScale = new Vector3(cylinder.localScale.x, scaleY, cylinder.localScale.z);
            cone.localPosition = new Vector3(cone.localPosition.x, -vectorLength * Mathf.Sign(scaleY) + vectorLength * scaleY, cone.localPosition.z);
            cone.localScale = new Vector3(cone.localScale.x, Mathf.Abs(cone.localScale.y) * Mathf.Sign(scaleY), cone.localScale.z);
        }

        private void rotateVector (Transform vector, float time) {
            float angle = (pendulum.localRotation.z > 0 ? 1 : -1) * Mathf.Atan(vectorChild(vectorAccTangent, false).localScale.y / vectorChild(vectorAccCenter, false).localScale.y) * 180.0f / Mathf.PI;
            Quaternion quaternion = Quaternion.Euler(0, 0, angle);
            vector.localRotation = quaternion;
        }
        private void rotateVectorCustom (Transform vector, float angle) {
            Quaternion quaternion = Quaternion.Euler(0, 0, angle);
            vector.rotation = quaternion;
        }

        private float getNscale (float t) {
            float vectNMin = g * Mathf.Abs(Mathf.Cos(angleAmplitude * Mathf.Deg2Rad));
            float vectNMax = g * (1 + Mathf.Abs(Mathf.Sin(angleAmplitude * Mathf.Deg2Rad)));
            float val = 2 * Mathf.PI * t / period;
            float vectNScale = vectNMin + (vectNMax - vectNMin) * Mathf.Abs(Mathf.Sin(val));
            return vectNScale;
        }

        private void updateValuesAndReset () {
            l = m_PendulumUIActionPanel.slLength.value;
            g = m_PendulumUIActionPanel.slGravity.value;
            angleAmplitude = -m_PendulumUIActionPanel.slAngle.value;
            accVectorAmp = g * Mathf.Abs(Mathf.Sin(angleAmplitude * Mathf.Deg2Rad));
            period = 2 * Mathf.PI * Mathf.Sqrt(l / (g * 10));
            scaleVectorCustom(vectorChild(vectorMg, false), vectorChild(vectorMg, true), g);
            scaleVector(vectorChild(vectorAcc, false), vectorChild(vectorAcc, true), 0, accVectorAmp, false, false);
            scaleVector(vectorChild(vectorF, false), vectorChild(vectorF, true), 0, accVectorAmp, false, false);
            startTime = Time.time;
        }
    }
}