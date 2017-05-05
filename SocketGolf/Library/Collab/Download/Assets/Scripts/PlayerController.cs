using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	// Use this for initialization
	Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer)
			return;
		
		if (Input.GetButtonDown ("Jump")) {
			rb.AddForce(new Vector3(0.0f, 4.0f, 0.0f), ForceMode.Impulse);
		}
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);
	}
}
