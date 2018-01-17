using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Controller : MonoBehaviour { 

	public int extra_enemy;
	private int[] spawn_array;

	public GameObject mines;
	public GameObject jet;
	public GameObject chopper;
	public GameObject goblin;
	public GameObject gob_rotator_left;
	public GameObject gob_rotator_right;
	public Vector3 mine_spawn_value;
	public Vector3 gob_rotator_spawn_value;
	public int mine_count;
	public float spawn_wait;
	public float start_wait;
	public float wave_wait;

	public Text score_text;
	private int score;

	//public GUIText restart_text;
	public Text gameOver_text;

	private bool /*restart,*/  gameOver;

	public GameObject restart_button;

	public float max_audio_volume;

	public GameObject shoot_pad;
	public GameObject movement_pad;

	public GameObject paused_panel;

	public Text paused_menu_loading;

	public GameObject player;
	public PlayerController PlayerController;


	public GameObject powerUp_life;
	public GameObject powerUp_killer;
	public GameObject powerUp_all_clear;
	public GameObject powerUp_3_bullet;


	IEnumerator Spawn_waves () {
		yield return new WaitForSeconds(start_wait);

		int wave_counter = 1;

		while (true) {

			clear_spawn_array();

			int jet_spawn_num =get_spwan_position_num();				//(int) Random.Range(0,mine_count+extra_enemy);

			int goblin_spawn_num =get_spwan_position_num();		/*(int) Random.Range(0,mine_count+extra_enemy);
																	for(int k = 0 ; k<mine_count+extra_enemy; k++) {
																		if(k!= jet_spawn_num)
																			break;
																		goblin_spawn_num =(int) Random.Range(0,mine_count+extra_enemy);
																	}*/

			int chopper_spawn_num =	get_spwan_position_num();							/*	(int) Random.Range(0,mine_count+extra_enemy);
											for(int k = 0 ; k<mine_count+extra_enemy; k++) {
												if(k!= jet_spawn_num && k!= goblin_spawn_num)
													break;
												chopper_spawn_num =(int) Random.Range(0,mine_count+extra_enemy);
											}*/

			//float gob_rotator_left_bool = Random.Range(0.0f,1.0f);
			//int gob_rotator_left_spawn_num = mine_count+2;

			int goblin_rotated_spawn_num = get_spwan_position_num();

			int gob_rotator_left_spawn_num = (int) Random.Range(0,mine_count+extra_enemy);
			int gob_rotator_right_spawn_num = (int) Random.Range(0,mine_count+extra_enemy);


			int power_up_spawn_num = mine_count+extra_enemy;
			int power_up_index = (int) Random.Range(0,5); //0->life,1->killer,2->all clear,3->3_bullet,4->nothing
			if(wave_counter % 2 == 0) {
				if(power_up_index==0) {
					if(PlayerController.get_life()< 6)
						power_up_spawn_num = get_spwan_position_num();
				}
				else if(power_up_index!=4)
					power_up_spawn_num = get_spwan_position_num();
			}



			for (int i =0; i<mine_count+extra_enemy; i++) {
					Vector3 spawn_position = new Vector3 (Random.Range (-mine_spawn_value.x, mine_spawn_value.x), mine_spawn_value.y, mine_spawn_value.z);
					Quaternion spawn_rotaion = Quaternion.identity;

					if(i == gob_rotator_left_spawn_num) {
						Vector3 gob_pos = new Vector3(gob_rotator_spawn_value.x * (-1.0f), gob_rotator_spawn_value.y,gob_rotator_spawn_value.z); 
						Instantiate (gob_rotator_left, gob_pos, spawn_rotaion);
					}

					if(i == gob_rotator_right_spawn_num) {
						Instantiate (gob_rotator_right, gob_rotator_spawn_value, spawn_rotaion);
					}

					if(i == jet_spawn_num)
						Instantiate (jet, spawn_position, spawn_rotaion);
					else if(i == goblin_spawn_num)
						Instantiate (goblin, spawn_position, spawn_rotaion);
					else if(i == chopper_spawn_num)
						Instantiate (chopper, spawn_position, spawn_rotaion);
					else if(i == goblin_rotated_spawn_num) {
						GameObject clone = Instantiate (goblin, spawn_position,spawn_rotaion ) as GameObject;
						clone.transform.LookAt(player.transform);
						clone.transform.Rotate (new Vector3(0.0f,180.0f,0.0f));
					}

					else if(i == power_up_spawn_num) {
						if(power_up_index==0)
							Instantiate (powerUp_life, spawn_position, spawn_rotaion);
						else if(power_up_index==1)
							Instantiate (powerUp_killer, spawn_position, spawn_rotaion);
						else if(power_up_index==2)
							Instantiate (powerUp_all_clear, spawn_position, spawn_rotaion);
						else if(power_up_index==3)
							Instantiate (powerUp_3_bullet, spawn_position, spawn_rotaion);
					}
	
					else
						Instantiate (mines, spawn_position, spawn_rotaion);


					yield return new WaitForSeconds (spawn_wait);
			}
			yield return new WaitForSeconds(wave_wait);

			if(gameOver) {
	//			restart_text.text = "Press 'R' to restart";
				restart_button.SetActive(true);
	//			restart = true;
				break;
			}

			wave_counter++;
		}
	}

	void Start () {
		float per = 100f;
		GetComponent<AudioSource>().volume = ((max_audio_volume * PlayerPrefs.GetFloat ("Volume")) / per );

		restart_button.SetActive (false);
		paused_panel.SetActive (false);
		Time.timeScale = 1.0f;
		gameOver = false;
//		restart = false;

		gameOver_text.text = "";
		paused_menu_loading.text = "";
	//	restart_text.text = "";

		if (PlayerPrefs.GetInt ("AutoFire") == 1)
				shoot_pad.SetActive(false);
		if (PlayerPrefs.GetFloat ("TiltDrag") == 0f)
				movement_pad.SetActive (false);

		score = 0;
		Update_Score ();
		spawn_array = new int[mine_count+extra_enemy];
		clear_spawn_array ();

		StartCoroutine(Spawn_waves());


	}

	void OnApplicationPause(bool pauseStatus) {
		//if (pauseStatus == false) {
				Time.timeScale = 0f;	
				paused_panel.SetActive (true);
		//}
	}



		void Update () {
			/*if (restart) {
				if(Input.GetKeyDown(KeyCode.R)) {
					//Application.Quit();
					Application.LoadLevel(Application.loadedLevel);
				}
			}*/
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu)) {
				//Time.timeScale = 0f;	
				//paused_panel.SetActive(true);	
				OnApplicationPause(true);
			}
		}



	public void Restart_Button_Pressed() {
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
		//Time.timeScale = 1;
		//restart_button.SetActive (false);
		//StartCoroutine(Spawn_waves());
		//player.SetActive (true);
	}

	public void Resume_Button_Pressed() {
		Time.timeScale = 1f;
		PlayerController.Calibrate_Accelerometer ();
		paused_panel.SetActive (false);
	}

	public void Menu_Button_Pressed() {
		Time.timeScale = 1f;
		paused_menu_loading.text = "Loading...";
		Application.LoadLevel ("menu");
	}

	public void Add_Score(int new_score) {
		score += new_score;
		Update_Score ();
	}

	void Update_Score() {
		score_text.text = "Score : " + score;
	}

	public void Game_is_over() {
		if(score > PlayerPrefs.GetInt ("HighScore"))
			PlayerPrefs.SetInt("HighScore",score);

		gameOver_text.text = "Game Over!!!";
		gameOver = true;


		restart_button.SetActive(true);
	}




	private void clear_spawn_array() {
		int i;
		for (i=0; i<mine_count+extra_enemy; i++)
			spawn_array [i] = 0;
	}

	private int  get_spwan_position_num() {
		int spawn_num =(int) Random.Range(0,mine_count+extra_enemy);

		while(spawn_num < mine_count+extra_enemy) {

			if(spawn_array[spawn_num]==0){
				spawn_array[spawn_num] = 1;
				break;
			}

			spawn_num =(int) Random.Range(0,mine_count+extra_enemy);
		}

		return spawn_num;
	}
	

}
