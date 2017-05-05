using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FBLoadName : MonoBehaviour {

	public GameObject DialogDisplayName;

	// Update is called once per frame
	void Update () {
		if (FB.IsLoggedIn) {
			Text displayName = DialogDisplayName.GetComponent<Text> ();
			displayName.text = FacebookManager.Instance.ProfileName;
		} else {
			Text displayName = DialogDisplayName.GetComponent<Text> ();
			displayName.text = "Login To Facebook";
			Debug.Log ("Not Logged in yet.");
		}
	}
}
