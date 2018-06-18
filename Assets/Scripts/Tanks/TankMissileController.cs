using System;
using System.Collections;
using UnityEngine;
namespace Tanks {
    public class TankMissileController : MonoBehaviour {
        [SerializeField] GameObject tankScrapPrefab;
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] GameObject m_BoomSound;
        TanksActionCamera m_TanksActionCamera;
        TanksController m_TanksController;
        GameObject audioSrc;
        bool destroyed = false;

        public void Init(TanksActionCamera tanksActionCamera, TanksController tanksController,Transform parent, float speed) {
            m_TanksActionCamera = tanksActionCamera;
            m_TanksController = tanksController;
            transform.parent = parent;
            //GetComponent<Rigidbody>().AddForce((transform.forward * speed), ForceMode.Impulse);
            GetComponent<Rigidbody>().velocity = (transform.forward * speed);
        }
        void Update() {
            Debug.DrawRay(transform.position, transform.forward);
            if (!GetComponent<Rigidbody>().velocity.normalized.Equals(Vector3.zero))
                transform.forward = GetComponent<Rigidbody>().velocity.normalized;
        }
        void OnCollisionEnter (Collision collision) {
            if (destroyed) return;
            m_TanksActionCamera.m_Target = null;
            m_TanksController.isMissileActive = false;
            doExplosion(collision);
        }
        private void doExplosion(Collision collision) {
            playSound();
            GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.Euler(Vector3.zero));
            destroyed = true;
            if (collision.collider.tag == "EnemyTank") {
                Transform tankTransform = collision.collider.transform.parent;
                GameObject scrapTank = GameObject.Instantiate(tankScrapPrefab, tankTransform.position, tankTransform.rotation) as GameObject;
                scrapTank.transform.parent = m_TanksController.transform;
                Destroy(tankTransform.gameObject);
            }
            Destroy(gameObject);
        }
        private void playSound () {
            audioSrc = Instantiate(m_BoomSound, transform.position, transform.rotation) as GameObject;
            audioSrc.GetComponent<AudioSource>().Play();
        }
    }
}