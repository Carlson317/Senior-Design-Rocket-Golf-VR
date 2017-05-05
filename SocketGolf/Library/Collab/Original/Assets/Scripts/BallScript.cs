using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallScript : NetworkBehaviour 
{
	public static bool GOAL = false;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
			GOAL = true;
		} 
	}
	bool checkGoalMade(){
		return GOAL;
	}
}
