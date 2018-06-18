using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class DestroyAudioPrefab : MonoBehaviour {
    AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if(!audioSource.isPlaying){
            Destroy(gameObject);
        }
    }
}
