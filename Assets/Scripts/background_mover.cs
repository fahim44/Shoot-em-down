using UnityEngine;
using System.Collections;

public class background_mover : MonoBehaviour {

	public float scoll_speed;

	private Vector2 saved_offset;

	// Use this for initialization
	void Start () {
		saved_offset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
		float y = Mathf.Repeat (Time.time * scoll_speed , 1);
		Vector2 offset = new Vector2 (saved_offset.x, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex",offset);
	}

	void OnDisable() {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex",saved_offset);
	}
}
