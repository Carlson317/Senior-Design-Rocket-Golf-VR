using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.forward = Camera.main.transform.forward;
		if (Camera.main != null) {
			Vector3 heading = Camera.main.transform.position - this.transform.position;
			this.transform.LookAt (this.transform.position - heading);
		}
	}
}
