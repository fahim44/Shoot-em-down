using UnityEngine;
using System.Collections;

public class Desttroy_By_Time : MonoBehaviour {
	public float life_time;

	public float max_audio_volume;
	// Use this for initialization
	void Start () {
		float per = 100f;
		GetComponent<AudioSource>().volume = ((max_audio_volume * PlayerPrefs.GetFloat ("Volume")) / per );

		Destroy (gameObject, life_time);
	}
}
