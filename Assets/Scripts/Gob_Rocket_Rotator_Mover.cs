using UnityEngine;
using System.Collections;

public class Gob_Rocket_Rotator_Mover : MonoBehaviour {

	public float tumble;
	
	void Update(){
		transform.Rotate (Vector3.up,tumble* Time.deltaTime);
	}
}
