using UnityEngine;

public static class Utils {

	public static class Audio{
		/// <summary>
		/// Plays an AudioClip on an instantiated AudioSource. After clip is finished, source is destroyed.
		/// </summary>
		/// <param name="mono">The GameObject, on which AudioSource will be instantiated.</param>
		/// <param name="clip">The AudioClip, that will be played.</param>
		/// <returns>Nothing. Void method.</returns>
		public static void playClip(MonoBehaviour mono, AudioClip clip) {
			AudioSource audioSource = Utils.Audio.getFreeAudioSource (mono);

			audioSource.clip = clip;
			audioSource.Play ();
		}
		/// <summary>
		/// Plays an AudioClip that will be randomly chosen from an array and played on an instantiated AudioSource.
		/// After clip is finished, source is destroyed.
		/// </summary>
		/// <remarks>The exception will be thrown if AudioClip array's length is 0 or 1.</remarks>
		/// <param name="mono">The GameObject, on which AudioSource will be instantiated.</param>
		/// <param name="clip">The array of AudioClips.</param>
		/// <returns>Nothing. Void method.</returns>
		public static void playClip(MonoBehaviour mono, AudioClip[] clip) {
			AudioSource audioSource = Utils.Audio.getFreeAudioSource(mono);

			int n = Random.Range(1, clip.Length);
			audioSource.clip = clip[n];
			clip[n] = clip[0];
			clip[0] = audioSource.clip;
			audioSource.Play();
		}

		public static AudioSource getFreeAudioSource(MonoBehaviour context) {
			AudioSource source = null;
			AudioSource[] sources = context.GetComponents<AudioSource>();
			for (int i = 0; i < sources.Length; i++) {
				if (!sources[i].isPlaying) {
					source = sources[i];
				}
			}
			if (source == null) {
				source = context.gameObject.AddComponent<AudioSource>();
				source.volume = 0.1f;
			}
			return source;
		}
	}
}
