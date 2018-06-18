using UnityEngine;
using System.Collections;

public class ParticleSystemPlayback : MonoBehaviour {
	public float playbackSpeed = 2.0f;
	void Start () {
		ParticleSystem system = GetComponent<ParticleSystem> ();
		if(system)
			system.playbackSpeed = playbackSpeed;
	}

}
