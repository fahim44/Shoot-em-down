﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	// Use this for initialization
	public float speed;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

}
