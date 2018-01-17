using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu_GameController : MonoBehaviour {

	public Slider tilt_drag_slide;
	public Slider volume_slide;
	public Text high_score_text;
	public Toggle auto_fire_toggle;

	public float max_audio_volume;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("HighScore"))
			high_score_text.text = "High Score : " + PlayerPrefs.GetInt ("HighScore").ToString ();
		else {
			high_score_text.text = "High Score : 0";
			PlayerPrefs.SetInt("HighScore",0);
		}

		if (PlayerPrefs.HasKey ("Volume"))
			volume_slide.value = PlayerPrefs.GetFloat ("Volume");
		else {
			volume_slide.value = 100f;
			PlayerPrefs.SetFloat("Volume",100f);
		}

		if (PlayerPrefs.HasKey ("TiltDrag"))
			tilt_drag_slide.value = PlayerPrefs.GetFloat ("TiltDrag");
		else {
			tilt_drag_slide.value = 0f;
			PlayerPrefs.SetFloat("TiltDrag",0f);
		}

		if (PlayerPrefs.HasKey ("AutoFire")) {
			if (PlayerPrefs.GetInt ("AutoFire") == 0)
					auto_fire_toggle.isOn = false;
			else
					auto_fire_toggle.isOn = true;
		} 
		else {
			auto_fire_toggle.isOn = true;
			PlayerPrefs.SetInt("AutoFire",1);
		}

	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();
	}



	public void start_game() {
		Application.LoadLevel("main");
	}

	public void exit_game() {
		Application.Quit ();
	}

	public void on_tilt_drag_change(float x) {
		PlayerPrefs.SetFloat("TiltDrag",x);
	}

	public void on_volume_change(float x) {
		PlayerPrefs.SetFloat("Volume",x);
		float per = 100f;
		GetComponent<AudioSource> ().volume = ((max_audio_volume * x) / per);
	}

	public void on_auto_fire_change(bool x) {
		if (x)
			PlayerPrefs.SetInt ("AutoFire", 1);
		else
			PlayerPrefs.SetInt ("AutoFire",0);
	}
}
