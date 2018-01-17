using UnityEngine;
using System.Collections;

public class MIne_Mover : MonoBehaviour {

	public float speed;
	public float tumble;
	// Use this for initialization
	void Start () {
		float direction = Random.Range (0.0f,1.0f);

		if (direction < 0.33f)
			direction = 0.0f;
		else if (direction < 0.66f)
			direction = tumble * -1.0f;
		else
			direction = tumble;

		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		GetComponent<Rigidbody> ().angularVelocity = new Vector3(0.0f,0.0f,direction);
	}
	

}
