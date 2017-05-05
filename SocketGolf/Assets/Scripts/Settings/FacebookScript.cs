using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FacebookScript : MonoBehaviour {

	public GameObject DialogLoggedIn;
	public GameObject DialogLoggedOut;
	public GameObject DialogUserName;
	public GameObject DialogProfilePic;

	void Awake(){
		FacebookManager.Instance.InitFB ();
		DealWithFBMenus (FB.IsLoggedIn);

	}
		
	public void FBlogin(){
		List<string> permissions = new List<string> ();
		permissions.Add ("public_profile");
		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result){
		if (result.Error != null) {
			Debug.Log (result.Error);
		} else {
			if (FB.IsLoggedIn) {
				FacebookManager.Instance.IsLoggedIn = true;
				FacebookManager.Instance.GetProfile ();
				Debug.Log ("FB is logged in");
				Debug.Log (Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
			} else {
				Debug.Log ("FB is not logged in");
			}
			DealWithFBMenus (FB.IsLoggedIn);
		}
	}

	void DealWithFBMenus(bool isLoggedIn){
		if (isLoggedIn) {
			DialogLoggedIn.SetActive (true);
			DialogLoggedOut.SetActive (false);
			if (FacebookManager.Instance.ProfileName != null) {
				Text Username = DialogUserName.GetComponent<Text> ();
				Username.text = "Welcome, " + FacebookManager.Instance.ProfileName;
			} else {
				//This may be called because the code ran so fast and didn't get the Profile info from Facebook API
				//StartCoroutine("WaitForProfileName");
				StartCoroutine (WaitForProfileName ());
			}
			if (FacebookManager.Instance.ProfilePic != null) {
				Image Profile_Pic = DialogProfilePic.GetComponent<Image> ();
				Profile_Pic.sprite = FacebookManager.Instance.ProfilePic;
			} else {
				StartCoroutine (WaitForProfilePicture ());

			}
			StartCoroutine (ReturnToMenu ());
		} else {
			DialogLoggedIn.SetActive (false);
			DialogLoggedOut.SetActive (true);
		}
	}

	IEnumerator WaitForProfileName(){ 
		while (FacebookManager.Instance.ProfileName == null) {
			yield return null;
		}

		DealWithFBMenus (FacebookManager.Instance.IsLoggedIn);
	}
	IEnumerator WaitForProfilePicture(){
		while (FacebookManager.Instance.ProfilePic == null) {
			yield return null;
		}

		DealWithFBMenus (FacebookManager.Instance.IsLoggedIn);
	}

	IEnumerator ReturnToMenu(){
		yield return new WaitForSecondsRealtime (3.0f);
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

	}
}




























