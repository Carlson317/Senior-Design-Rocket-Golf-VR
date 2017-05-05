using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrag : MonoBehaviour {

	public Rigidbody rb;
	float scaler = 0.1f;
	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.sleepThreshold = .01f;
	}
	void Update() {
	}

	void FixedUpdate(){
		rb.inertiaTensorRotation = Quaternion.Euler(0.01f, 0.01f, 0.01f);
		rb.AddTorque(-rb.angularVelocity * scaler);
	}
}
