using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin,zMax;
}

public class PlayerController : MonoBehaviour {

	public float tilt_speed;
	public float drag_speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shot_spwan;

	public float fireRate;
	private float nextFire;

	private Quaternion calibration_quaternion;

	public float max_audio_volume;

	public Shoot_Pad shoot_pad;
	public Movement_pad movement_pad;

	public int initial_life;
	private int current_life;

	public GameObject left_damage_particle;
	public GameObject right_damage_particle;
	public GameObject get_damaged_particle;

	public Life_Logo  life_logo;

	public float bullet_3_spawning_angel;
	public float bullet_3_active_time;
	private float bullet_3_timer;
	private bool is_bullet_3_active;



/// <summary>
///  functions   /// </summary>
	void Start() {
		float per = 100f;
		current_life = initial_life;

		left_damage_particle.SetActive(false);
		right_damage_particle.SetActive(false);
		get_damaged_particle.SetActive (false);

		is_bullet_3_active = false;

		GetComponent<AudioSource>().volume = ((max_audio_volume * PlayerPrefs.GetFloat ("Volume")) / per );

		if (PlayerPrefs.GetFloat ("TiltDrag") == 0f)
			Calibrate_Accelerometer ();
	}

	public void Calibrate_Accelerometer() {
		Vector3 accelerate_snapshot = Input.acceleration;
		Quaternion rotate_quaternion = Quaternion.FromToRotation (new Vector3(0.0f, 0.0f, -1.0f),  accelerate_snapshot);
		calibration_quaternion = Quaternion.Inverse (rotate_quaternion);
	}

	Vector3 Fix_acceleration(Vector3 acceleration) {
		Vector3 fix_acceleration = calibration_quaternion * acceleration;				// quaternion on left -_-
		return fix_acceleration;
	}

	// Use this for initialization
	void FixedUpdate(){
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 movement;

		if (PlayerPrefs.GetFloat ("TiltDrag") == 0f) {
			Vector3 acceleration_raw = Input.acceleration;
			Vector3 acceleration = Fix_acceleration (acceleration_raw);
			movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
			GetComponent<Rigidbody>().velocity = movement * tilt_speed;
		}
		else {
			Vector2 direction = movement_pad.GetDirection();
			movement = new Vector3 (direction.x, 0.0f, direction.y);
			GetComponent<Rigidbody>().velocity = movement * drag_speed;
		}

		//GetComponent<Rigidbody>().velocity = movement * speed;
		//rigidbody.AddForce (movement * Time.deltaTime * speed);

		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * (-tilt));
	}




	void Update() {
		if (/*(Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && */ Time.time > nextFire) {
			if(PlayerPrefs.GetInt ("AutoFire") == 1 || (PlayerPrefs.GetInt ("AutoFire") == 0 && shoot_pad.CanFire())) {
				nextFire = Time.time + fireRate;
				Instantiate (shot,shot_spwan.position,Quaternion.Euler (0.0f, 0.0f, 0.0f));

				if(is_bullet_3_active) {
					if(bullet_3_timer > Time.time) {
						Instantiate (shot,shot_spwan.position,Quaternion.Euler (0.0f, bullet_3_spawning_angel, 0.0f));
						Instantiate (shot,shot_spwan.position,Quaternion.Euler (0.0f, -bullet_3_spawning_angel, 0.0f));
					}
					else
						is_bullet_3_active = false;
				}
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public void set_life(int i){

		if (i < current_life)
			get_damaged_particle.SetActive (true);

		current_life = i;
		life_logo.set_logo (current_life);

		if (current_life == 2) {
			left_damage_particle.SetActive (true);
			right_damage_particle.SetActive(false);
		}
		else if (current_life == 1)
			right_damage_particle.SetActive(true);
		else {
			left_damage_particle.SetActive(false);
			right_damage_particle.SetActive(false);
		}
	}

	public int get_life(){
		return current_life;
	}


	public void start_3_bullet() {
		bullet_3_timer = bullet_3_active_time + Time.time;
		is_bullet_3_active = true;
	}
}
