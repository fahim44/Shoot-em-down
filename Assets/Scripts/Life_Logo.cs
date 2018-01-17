using UnityEngine;
using System.Collections;

public class Life_Logo : MonoBehaviour {

	public GameObject life_1;
	public GameObject life_2;
	public GameObject life_3;
	public GameObject life_4;
	public GameObject life_5;
	public GameObject life_6;

	public GameObject life_explosion;

	private int currently_on;

	// Use this for initialization
	void Start () {
		life_1.SetActive (true);
		life_2.SetActive (true);
		life_3.SetActive (true);

		life_4.SetActive (false);
		life_5.SetActive (false);
		life_6.SetActive (false);

		currently_on = 3;
	}
	
	public void set_logo(int i) {

		if (i == 6) {
				life_6.SetActive(true);
				life_6.transform.rotation = life_1.transform.rotation;
		}

		else if (i == 5) {
			if(i<currently_on) {
				life_6.SetActive(false);
				Instantiate (life_explosion, life_6.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}
			else {
				life_5.SetActive(true);
				life_5.transform.rotation = life_1.transform.rotation;
			}

		}

		else if (i == 4) {
			if(i<currently_on) {
				life_5.SetActive(false);
				Instantiate (life_explosion, life_5.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}
			else {
				life_4.SetActive(true);
				life_4.transform.rotation = life_1.transform.rotation;
			}
			
		}

		else if (i == 3) {
			if(i<currently_on) {
				life_4.SetActive(false);
				Instantiate (life_explosion, life_4.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}
			else {
				life_3.SetActive(true);
				life_3.transform.rotation = life_1.transform.rotation;
			}
			
		}

		else if (i == 2) {
			if(i<currently_on) {
				life_3.SetActive(false);
				Instantiate (life_explosion, life_3.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}
			else {
				life_2.SetActive(true);
				life_2.transform.rotation = life_1.transform.rotation;
			}
			
		}

		else if (i == 1) {
			if(i<currently_on) {
				life_2.SetActive(false);
				Instantiate (life_explosion, life_2.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}
			else {
				life_1.SetActive(true);
				life_1.transform.rotation = life_1.transform.rotation;
			}
			
		}

		/*
		if (i == 2) {
			life_3.SetActive(false);
			Instantiate (life_explosion, life_3.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		}
		else if(i == 1) {
			//life_3.SetActive(false);
			life_2.SetActive(false);
			Instantiate (life_explosion, life_2.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		}*/
		else if(i==0) {


			//life_2.transform.rotation = life_1.transform.rotation;


			life_1.SetActive(false);
			Instantiate (life_explosion, life_1.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		}

		currently_on = i;
	}


	public void kill_all() {
		life_1.SetActive (false);
		life_2.SetActive (false);
		life_3.SetActive (false);
		
		life_4.SetActive (false);
		life_5.SetActive (false);
		life_6.SetActive (false);

		if(currently_on >= 1)
			Instantiate (life_explosion, life_1.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		if(currently_on >= 2)
			Instantiate (life_explosion, life_2.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		if(currently_on >= 3)
			Instantiate (life_explosion, life_3.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		if(currently_on >= 4)
			Instantiate (life_explosion, life_4.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		if(currently_on >= 5)
			Instantiate (life_explosion, life_5.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
		if(currently_on >= 6)
			Instantiate (life_explosion, life_6.transform.position, Quaternion.Euler (0.0f, 0.0f, 0.0f));

		currently_on = 0;
	}
}
