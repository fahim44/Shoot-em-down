using UnityEngine;
using System.Collections;

public class Destroy_by_boundary : MonoBehaviour {

	// Use this for initialization
	void OnTriggerExit(Collider other) {
		Destroy (other.gameObject);
	}
}
