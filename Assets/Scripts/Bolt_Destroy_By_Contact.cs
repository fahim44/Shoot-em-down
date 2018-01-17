using UnityEngine;
using System.Collections;

public class Bolt_Destroy_By_Contact : MonoBehaviour {

	public GameObject explosion;
	public GameObject player_explosion;

	private Game_Controller gameController;

	private PlayerController playerController;
	
	
	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<Game_Controller>();		
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");		
		}

		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController>();		
		}
		if (playerController == null) {
			Debug.Log ("Cannot find 'GameController' script");		
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

				playerController.set_life (playerController.get_life () - 1);
				if (playerController.get_life () < 1) {
						Instantiate (player_explosion, transform.position, transform.rotation);
						gameController.Game_is_over ();
						Destroy (other.gameObject);
				}

				Instantiate (explosion, transform.position, transform.rotation);
				//Instantiate (player_explosion,transform.position,transform.rotation);

				//gameController.Game_is_over();

				//Destroy (other.gameObject);
				Destroy (gameObject);
		} 

		else if (other.tag == "All Clear") {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
