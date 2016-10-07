using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	static public SoundManager instance = null;
	public AudioSource MusicSource;
	public AudioSource SfxSource;


	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	
	public void PlaySfx(AudioClip clip){
		SfxSource.PlayOneShot (clip);
	}

	public void ToggleVolume(){
		MusicSource.volume = MusicSource.volume == 0f ? 1f : 0f;
		SfxSource.volume = SfxSource.volume == 0f ? 1f : 0f;
	}
	public void ResetMusic(){
		MusicSource.Stop ();
		MusicSource.Play ();
	}
}
