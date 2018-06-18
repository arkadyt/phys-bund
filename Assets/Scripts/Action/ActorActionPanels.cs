using PlayerControllers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Action {
    public class ActorActionPanels : MonoBehaviour {
        public ActorController m_ActorController;
        public GameObject m_PressEPanel;
        public GameObject m_CloseActionPanel;
        [SerializeField] GameObject m_AppPanel;
        [SerializeField] GameObject m_InfoPanel;
        [SerializeField] GameObject m_MenuPanel;

        public bool isEactive = false;

        public void OnCloseActionPressed() {
            m_ActorController.m_ActionController.m_ActionObject.exitAction();
        }
        public void OnInfoPressed(bool open) {
            m_InfoPanel.SetActive(open);
        }

        public void OnCloseAppPressed() {
            Application.Quit();
            #if UNITY_EDITOR
                if (EditorApplication.isPlaying) EditorApplication.isPlaying = false;
            #endif

        }
        public void openEPanel(bool open) {
            m_PressEPanel.SetActive(open && !m_MenuPanel.GetComponent<FadingPanel>().isOpened? isEactive : false);
        }
        public void OnMenuOpen(bool open) {
            m_ActorController.enabled = !open;
            m_ActorController.iCtrl.showCursor(open);
            m_MenuPanel.GetComponent<FadingPanel>().openMenu(open);
            openEPanel(!open); // must be after FadingPanel.openMenu(open)
        }
        void Update() {
            if (m_ActorController.iCtrl.isMainMenuPressed()) {
                if (m_ActorController.m_ActionController.isInAction) {
                    m_ActorController.m_ActionController.m_ActionObject.exitAction();
                }
                else {
                    OnMenuOpen(!m_MenuPanel.GetComponent<FadingPanel>().isOpened);
                }
            }
        }
    }
}