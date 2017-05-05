using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class SetUpSinglePlayer : MonoBehaviour
{
	public Color pColor = Color.blue;
	private Camera topDown;
	private Camera main;
	private GvrHead g;
	IEnumerator Start(){
		Camera.main.transform.position = this.transform.position - this.transform.forward * 7 + this.transform.up * 2;
		//Camera.main.transform.position = this.transform.position + this.transform.up * 2;

		Camera.main.transform.LookAt (this.transform.position);
		Camera.main.transform.parent = this.transform;
		g = Camera.main.GetComponent<GvrHead>();
		this.GetComponent<MeshRenderer> ().material.color = pColor;

		yield return new WaitUntil (mapGenerated);
		this.transform.position = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));
		RaycastHit h;
		float heightAboveGround = 0f;
		if (Physics.Raycast (this.transform.position, this.transform.TransformDirection (Vector3.down) ,out h, Mathf.Infinity))
		{
			heightAboveGround = h.distance;
			Debug.Log ("Player height in the air " + heightAboveGround);
		}
		this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - heightAboveGround +2.5f), this.transform.position.z);

	}
	bool mapGenerated()
	{
		return(MapGenerator.mapMade || SingleMapGenerator.mapMade);
	}
}

