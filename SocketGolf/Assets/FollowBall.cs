using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {

	public GameObject objectToFollow;
	// Use this for initialization
	void Start () {
		transform.position = objectToFollow.transform.position + new Vector3 (0f, 25f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = objectToFollow.transform.position + new Vector3 (0f, 25f, 0f);
	}
}
