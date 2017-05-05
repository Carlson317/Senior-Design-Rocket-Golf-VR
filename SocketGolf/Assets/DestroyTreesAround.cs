using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTreesAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//Destroy any trees we are in contact with
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("COLLISION!!!!!!!!!!!!!" + collision.gameObject.name);
		if ( collision.gameObject.name == "Tree3(Clone)" )
		{
			Destroy(collision.gameObject);
		}
	}
}
