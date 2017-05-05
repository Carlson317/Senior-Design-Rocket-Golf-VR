using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	//Singleton Class
	private static FacebookManager _instance;

	public static FacebookManager Instance{
		get{
			if (_instance == null) {
				GameObject fbm = new GameObject ("FBManager");
				fbm.AddComponent<FacebookManager> ();
			}

			return _instance;
		}
	}

	public bool IsLoggedIn{ get; set; }
	public string ProfileName { get; set; }
	public Sprite ProfilePic { get; set; }


	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		_instance = this;
		IsLoggedIn = true;
	}

	public void InitFB(){
		if (!FB.IsInitialized)
			FB.Init (SetInit, OnHideUnity);
		else
			IsLoggedIn = FB.IsLoggedIn;
	}

	void SetInit(){

		if (FB.IsLoggedIn) {
			Debug.Log ("FB is logged in");
			GetProfile ();
		} else {
			Debug.Log ("FB is not logged in");
		}
		IsLoggedIn = FB.IsLoggedIn;
	}

	void OnHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;

		}
	}

	public void GetProfile(){
		FB.API ("/me?fields=name", HttpMethod.GET, DisplayUserName);
		FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
	}

	void DisplayUserName(IResult result){

		if (result.Error == null){
			ProfileName = result.ResultDictionary ["name"].ToString();
		}else{
			Debug.Log(result.Error);
		}
	}

	void DisplayProfilePic(IGraphResult result){
		if (result.Texture != null) {

			ProfilePic = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}
	}
}

