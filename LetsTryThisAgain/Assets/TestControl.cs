using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetButtonDown ("Jump")) {
		//	transform.Rotate (transform.rotation.eulerAngles + new Vector3 (0f, 0.01f, 0f));
		//}
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float j = Input.GetAxis ("Jump");
		transform.position += this.transform.forward * v * Time.deltaTime;
		transform.Rotate (new Vector3 (0f, h, 0f) * 75.0f * Time.deltaTime);
		//transform.position += new Vector3 (0f, -j * Time.deltaTime, 0f);
	}
}
