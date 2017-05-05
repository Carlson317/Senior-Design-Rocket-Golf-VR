using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour{

	[SyncVar]
	public string pName = "player";

	[SyncVar]
	public Color pColor = Color.white;

	private Camera topDown;
	private Camera main;
	private GvrHead g;

	void Start (){
		if (isLocalPlayer) {
			//Allows the PLayer to Move around... Only if connected
			//GetComponent<PlayerMove> ().enabled = true;

			//This fixed our camera problem.. No longer need a camera on the prefab.. 
			//Instead the main camera is duplicated and then forced to be on our bean guy
			//VR set up on the Main Camera Inspection..
			Camera.main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 2;
			//Camera.main.transform.position = this.transform.position + this.transform.up * 2;

			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
			g = Camera.main.GetComponent<GvrHead>();
			/*
			foreach (Camera c in Camera.allCameras)
			{
				Debug.Log (c.gameObject.name);
				if (c.gameObject.name == "TopDown") {
					topDown = c;
					topDown.transform.position = this.transform.position + new Vector3 (0f, 10f, 0f);
					topDown.transform.LookAt (this.transform.position);
					topDown.transform.parent = this.transform;
				} else if (c.gameObject.name == "Main Camera") {
					main = c;
					main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 2;
					main.transform.LookAt (this.transform.position);
					main.transform.parent = this.transform;
				}
			}*/


//			CmdChangeName (pName);
			//Gets the Text above guy and guy and sets the color to whatever they choose
			this.GetComponentInChildren<TextMesh> ().text = pName;
			this.GetComponent<MeshRenderer> ().material.color = pColor;
		} else {
			this.GetComponentInChildren<TextMesh> ().text = pName;
			this.GetComponent<MeshRenderer> ().material.color = pColor;
		}
			
		//Create a random spawn point for the bean dudes
		this.transform.position = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));			
	}

	void Update(){
		if (Input.GetKeyDown ("l") || Input.GetKeyDown (KeyCode.JoystickButton3)) {
			g.trackRotation = false;
			Camera.main.transform.position = this.transform.position - this.transform.forward * 2 + new Vector3 (0f, 10f, 0f);
			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
		}
		else if(Input.GetKeyUp("l") || Input.GetKeyup (KeyCode.JoystickButton3)){
			Camera.main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 2;
			//Camera.main.transform.position = this.transform.position + this.transform.up * 2;

			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
			g.trackRotation = true;
		}
		else if (Input.GetKeyDown ("m") || Input.GetKeyDown (KeyCode.JoystickButton1)) {
			g.trackRotation = false;
			Camera.main.transform.position = this.transform.position + this.transform.forward * 2 + this.transform.right * 10 + new Vector3 (0f, 3f, 0f);
			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
		}
		else if(Input.GetKeyUp("m") || Input.GetKeyUp (KeyCode.JoystickButton1)){
			Camera.main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 2;
			//Camera.main.transform.position = this.transform.position + this.transform.up * 2;

			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
			g.trackRotation = true;
		}
		else if (Input.GetKeyDown ("n") || Input.GetKeyDown (KeyCode.JoystickButton2)) {
			g.trackRotation = false;
			Camera.main.transform.position = this.transform.position + this.transform.forward * 2 - this.transform.right * 10 + new Vector3 (0f, 3f, 0f);
			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
		}
		else if(Input.GetKeyUp("n") || Input.GetKeyUp (KeyCode.JoystickButton2)){
			Camera.main.transform.position = this.transform.position - this.transform.forward * 5 + this.transform.up * 2;
			//Camera.main.transform.position = this.transform.position + this.transform.up * 2;

			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;
			g.trackRotation = true;
		}
	}
}
