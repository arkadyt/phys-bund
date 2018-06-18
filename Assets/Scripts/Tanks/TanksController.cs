using UnityEngine;

namespace Tanks {
    public class TanksController : MonoBehaviour {
        [SerializeField] TanksActionUIPanel m_TanksActionUIPanel;
        [SerializeField] TanksActionCamera m_TanksActionCamera;
        [SerializeField] GameObject m_MissilePrefab;
        [SerializeField] Transform m_SpawnTransform;
        [SerializeField] Transform m_Barrel;
        [SerializeField] GameObject m_ShootSound;
        [HideInInspector] public bool canShoot = false;
        [HideInInspector] public bool isMissileActive = false;
        GameObject audioSource;
        GameObject missileObj;

        void Update () {
            if (!isMissileActive && canShoot && Input.GetButton("ShootMissile"))
                shoot();
        }

        public void shoot () {
            playSound();
            isMissileActive = true;
            missileObj = Instantiate(m_MissilePrefab, m_SpawnTransform.position, m_Barrel.rotation) as GameObject;
            TankMissileController controller = missileObj.GetComponent<TankMissileController>();
            controller.Init(m_TanksActionCamera, this, transform, getStartSpeed());
            m_TanksActionCamera.m_Target = missileObj.transform;
        }
        private void playSound () {
            audioSource = Instantiate(m_ShootSound, m_SpawnTransform.position, m_SpawnTransform.rotation) as GameObject;
            audioSource.transform.parent = m_SpawnTransform.transform;
            audioSource.GetComponent<AudioSource>().Play();
        }

        public void OnAngleChanged() {
            m_TanksActionUIPanel.txtAngle.text = m_TanksActionUIPanel.slAngle.value + "";
            m_Barrel.localRotation = Quaternion.Euler(new Vector3(-m_TanksActionUIPanel.slAngle.value, 0, 0));
        }
        public void OnSpeedChanged() {
            m_TanksActionUIPanel.txtSpeed.text = m_TanksActionUIPanel.slSpeed.value + "";
        }
        public float getStartSpeed() {
            return m_TanksActionUIPanel.slSpeed.value/10.0f;
        }
        public void OnRestart() {
            //Application.LoadLevel(Application.loadedLevel);
        }
    }
}