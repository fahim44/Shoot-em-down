using UnityEngine;
using System.Collections;

public class PowerUp_Controller : MonoBehaviour {

	public GameObject player_explosion;

	public GameObject all_clear;

	private PlayerController playerController;
	private Game_Controller gameController;
	private Life_Logo life_logo_controller;

	// Use this for initialization
	void Start () {

		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController>();		
		}

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<Game_Controller>();		
		}

		GameObject life_logo_controller_object = GameObject.FindWithTag ("Life Bar");
		if (life_logo_controller_object != null)
			life_logo_controller = life_logo_controller_object.GetComponent<Life_Logo> ();

			

	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if(tag == "Life PowerUp") {
				playerController.set_life(playerController.get_life()+1);
			}

			else if(tag == "Killer") {
				life_logo_controller.kill_all();

				Instantiate (player_explosion,transform.position,transform.rotation);
				gameController.Game_is_over();
				Destroy (other.gameObject);
			}

			else if(tag == "All Clear PowerUp") {
				Instantiate (all_clear,new Vector3(0.0f,0.0f,-7.0f),Quaternion.Euler (0.0f, 0.0f, 0.0f));
			}

			else if(tag == "Bullet 3 PowerUp") {
				playerController.start_3_bullet();
			}

			Destroy(gameObject);
		}
	}


}
