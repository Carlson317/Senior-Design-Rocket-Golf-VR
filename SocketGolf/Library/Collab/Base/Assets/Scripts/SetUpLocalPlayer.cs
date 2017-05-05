using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour{

	[SyncVar]
	public string pName = "player";

	[SyncVar]
	public Color pColor = Color.white;

	void Start (){
		if (isLocalPlayer) {

			//Allows the PLayer to Move around... Only if connected
			//GetComponent<PlayerMove> ().enabled = true;

			//This fixed our camera problem.. No longer need a camera on the prefab.. 
			//Instead the main camera is duplicated and then forced to be on our bean guy
			//VR set up on the Main Camera Inspection..
			Camera.main.transform.position = this.transform.position - this.transform.forward * 10 + this.transform.up * 3;
			Camera.main.transform.LookAt (this.transform.position);
			Camera.main.transform.parent = this.transform;

//			CmdChangeName (pName);
			//Gets the Text above guy and guy and sets the color to whatever they choose
			this.GetComponentInChildren<TextMesh> ().text = pName;
			this.GetComponent<MeshRenderer> ().material.color = pColor;
		}
			
		//Create a random spawn point for the bean dudes
		this.transform.position = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));
			
	}

	void Update(){
		this.GetComponentInChildren<TextMesh> ().text = pName;
		this.GetComponent<MeshRenderer> ().material.color = pColor;
	}
}
