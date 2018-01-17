using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Shoot_Pad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	private bool touched;
	private int pointer_id;
	private bool can_fire;

	// Use this for initialization
	void Awake () {
		touched = false;
	}
	
	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointer_id = data.pointerId;
			can_fire = true;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointer_id) {
			can_fire = false;
			touched =false;
		}
	}

	public bool CanFire() {
		return can_fire;
	}
}
