using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	Camera c;
	// Use this for initialization
	void Start () {
		if (Network.isServer) {
			Camera.main.enabled = true;
		}
		if (Network.isClient) {
			Camera.main.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
