using UnityEngine;
using System.Collections;

public class set_active_false : MonoBehaviour {
	public float active_time;
	private float stop_time;


	// Use this for initialization
	void Start () {
		stop_time = active_time + Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > stop_time) {
			//GetComponent<ParticleSystem> ().Stop();
			gameObject.SetActive (false);
		}
	}

	void OnEnable() {
		stop_time = active_time + Time.time;
		//GetComponent<ParticleSystem> ().Simulate (2.0f, true, true);
		GetComponent<ParticleSystem> ().Clear ();
		//GetComponent<ParticleSystem> ().time = 0.0f;
		GetComponent<ParticleSystem> ().Play ();
	}
}
