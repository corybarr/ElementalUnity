using UnityEngine;
using System.Collections;

public class SoundMaker : MonoBehaviour {
	public AudioClip clip;
	public bool playNote = false;
	private AudioSource source;
	
	void Start () {
		gameObject.AddComponent ("AudioSource");
		source = GetComponent(typeof(AudioSource)) as AudioSource;
		source.playOnAwake = false;
		source.loop = false;
		source.clip = clip;
	}
	
	void PointCloudCollisionEnter() {
		if (playNote) {
			source.pitch = Random.Range(0.5f, 1.0f);
			source.volume = Random.Range(0.2f, 1.0f);
			source.Play();
		}
	}
}
