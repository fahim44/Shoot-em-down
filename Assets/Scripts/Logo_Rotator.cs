using UnityEngine;
using System.Collections;

public class Logo_Rotator : MonoBehaviour {

	public float tumble;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = new Vector3(0.0f,0.0f,tumble);
	}
	

}
