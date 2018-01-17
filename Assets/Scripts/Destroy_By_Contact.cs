using UnityEngine;
using System.Collections;

public class Destroy_By_Contact : MonoBehaviour {

	public GameObject explosion;
	public GameObject player_explosion;

	public int mine_score_value;
	public int jet_score_value;
	public int goblin_score_value;
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
		if (other.tag != "Boundary" && (other.tag == "Bolt" || other.tag == "Player" || other.tag == "All Clear")) {

			if(tag != "Enemy_Jet") {
				Instantiate (explosion,transform.position,transform.rotation);
			}

			if (other.tag == "Player") {
				playerController.set_life(playerController.get_life()-1);
				if(playerController.get_life()<1) {
					Instantiate (player_explosion,transform.position,transform.rotation);
					gameController.Game_is_over();
					Destroy (other.gameObject);
				}
			}

			if(tag == "Mines")
				gameController.Add_Score(mine_score_value);
			else if(tag == "Goblin")
				gameController.Add_Score(goblin_score_value);
			else 
				gameController.Add_Score(jet_score_value);

			if (other.tag != "Player" && other.tag != "All Clear")
				Destroy (other.gameObject);

			if(tag == "Enemy_Jet"){
				int i = GetComponent<Enemy_life>().get_life();

				Instantiate (explosion,transform.position,transform.rotation);

				if(i>1 && other.tag != "All Clear")
					GetComponent<Enemy_life>().set_life(i-1);
				else {
					Destroy (gameObject);
				}
			}
			else
				Destroy (gameObject);
		}
	}
}
