using UnityEngine;
using System.Collections;

public class Shoot_Cannon_Ball : MonoBehaviour {

	public GameObject shot;
	public Transform shot_spwan;
	
	public float fireRate;
	public float nextFire;
	public float shoot_angle;
	
	public float max_audio_volume;

	void Start() {
		float per = 100f;
		GetComponent<AudioSource>().volume = ((max_audio_volume * PlayerPrefs.GetFloat ("Volume")) / per );
	}
	
	void Update() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot,shot_spwan.position,Quaternion.Euler (0.0f, shoot_angle, 0.0f));
			Instantiate (shot,shot_spwan.position,Quaternion.Euler (0.0f, -shoot_angle, 0.0f));
			GetComponent<AudioSource>().Play();
		}
	}
}
