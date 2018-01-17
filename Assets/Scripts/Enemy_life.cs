using UnityEngine;
using System.Collections;

public class Enemy_life : MonoBehaviour {

	public int max_life;
	private int current_life;

	public GameObject damage_particle;

	// Use this for initialization
	void Start () {
		current_life = max_life;
		damage_particle.SetActive (false);
	}
	
	public void set_life(int i) {
		current_life = i;
		damage_particle.SetActive (true);
	}

	public int get_life() {
		return current_life;
	}
}
