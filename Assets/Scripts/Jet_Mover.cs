using UnityEngine;
using System.Collections;


[System.Serializable]
public class Jet_Boundary {
	public float xMin, xMax;
}

public class Jet_Mover : MonoBehaviour {

	// Use this for initialization
	public float speed;
	private float timer;
	public float tilt;
	public float start_angle_time;
	public float stop_angle_time;

	public Jet_Boundary boundary;

	private float tilt_direction;



	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		timer = Time.time;

		tilt_direction = Random.Range (0.0f,1.0f);
		if (tilt_direction < 0.33f)
			tilt_direction = speed; // left
		else if (tilt_direction < 0.66f)
			tilt_direction = speed * -1.0f; //right
		else
			tilt_direction = 0.0f; //straight
	}

	void FixedUpdate() {
		if ((Time.time - timer) > start_angle_time && (Time.time - timer) < stop_angle_time) 
			GetComponent<Rigidbody> ().velocity = new Vector3 (tilt_direction,0.0f,speed );
		else
			GetComponent<Rigidbody>().velocity = transform.forward * speed;

		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			GetComponent<Rigidbody>().position.z
			);


		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * (-tilt));
	}
}
