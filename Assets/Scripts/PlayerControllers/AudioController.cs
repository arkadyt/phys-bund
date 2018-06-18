using UnityEngine;
using System;

namespace PlayerControllers{
	[Serializable]
	public class AudioController {
		[Serializable]
		public class AudioClips {
			public AudioClip[] FootstepSounds;
			public AudioClip JumpSound;
			public AudioClip LandSound;
		}
		[SerializeField] public AudioClips m_AudioClips;
		private MonoBehaviour refMono;

		public void Init(MonoBehaviour pMono){
			refMono = pMono;
		}

		public void playClip(AudioClip pClip){
			Utils.Audio.playClip(refMono, pClip);
		}
		public void playClip(AudioClip[] pClips){
			Utils.Audio.playClip(refMono, pClips);
		}
	}
}
