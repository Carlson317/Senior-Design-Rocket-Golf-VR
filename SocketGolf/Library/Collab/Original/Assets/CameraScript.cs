using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraScript : NetworkBehaviour {
	// Use this for initialization
	void Start () {
		NetworkView n = this.gameObject.GetComponent<NetworkView>();
		if(n.isMine){
			this.GetComponent<Camera>().enabled = true;
		}
		else{
			this.GetComponent<Camera>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
