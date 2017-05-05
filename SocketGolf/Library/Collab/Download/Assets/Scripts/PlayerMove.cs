using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {
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

		this.gameObject.transform.Rotate (0f, x, 0f);
		this.gameObject.transform.Translate (0f, 0f, z);
	}
}
