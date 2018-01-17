using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Movement_pad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smooth_direction;

	private bool touched;
	private int pointer_id;

	// Use this for initialization
	void Awake () {
		direction = Vector2.zero;
		touched = false;
	}
	
	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointer_id = data.pointerId;
			origin = data.position;

		}
	}

	public void OnDrag (PointerEventData data) {
		if (data.pointerId == pointer_id) {
			Vector2 current_position = data.position;
			Vector2 direction_raw = current_position - origin;
			direction = direction_raw.normalized;
		}
	}
	
	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointer_id) {
			direction = Vector2.zero;
			touched =false;
		}
	}

	public Vector2 GetDirection () {
		smooth_direction = Vector2.MoveTowards (smooth_direction, direction, smoothing);
		return smooth_direction;
	}
}
