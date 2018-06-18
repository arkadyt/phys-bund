using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class FadingPanel : MonoBehaviour {
    [SerializeField] private float m_FadeTime = 1f;
    [HideInInspector]public bool isOpened = false;
    private bool isOpening = false;
    private CanvasGroup m_CanvasGroup;
    void Awake() {
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }
    public void openMenu(bool open) {
        if (isOpening) return;
        StartCoroutine(CrossFadeAlpha(open));
    }
    IEnumerator CrossFadeAlpha (bool open) {
        float i = 0.0f;
        isOpening = true;
        isOpened = open;
        while (i < m_FadeTime) {
            i += Time.deltaTime ;
            m_CanvasGroup.alpha = open ? i / m_FadeTime : 1 - i / m_FadeTime;
            yield return null;
        }
        isOpening = false;
        m_CanvasGroup.interactable = open;
    }
}
