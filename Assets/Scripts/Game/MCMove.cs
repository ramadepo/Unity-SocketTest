using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	//control the player's movement
		if (Input.GetKey(KeyCode.LeftArrow)) {
			gameObject.transform.Translate (new Vector3 (-0.3f, 0f, 0f));
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			gameObject.transform.Translate (new Vector3 (0.3f, 0f, 0f));
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			gameObject.transform.Translate (new Vector3 (0f, 0f, 0.3f));
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			gameObject.transform.Translate (new Vector3 (0f, 0f, -0.3f));
		}
	}
}
