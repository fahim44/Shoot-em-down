using UnityEngine;
using System.Collections;

public class Goblin_Mover : MonoBehaviour {

	public float speed;
	public float top_speed;
	public float top_speed_time;

	private float timer;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		timer = Time.time;
	}

	void FixedUpdate() {
		if ((Time.time - timer) > top_speed_time) 
			GetComponent<Rigidbody>().velocity = transform.forward * top_speed;
	}
}
