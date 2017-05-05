using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Countdown : MonoBehaviour {

	public float countdownTime;

	[SerializeField]
	Text countdownUI;

	// Use this for initialization
	void Start () {
		countdownUI = GameObject.Find ("Countdown").GetComponent<Text> ();
		countdownUI.text = countdownTime.ToString();
		Debug.Log ("Here!!?");
		//CountdownCoroutine ();
		StartCoroutine(CountdownCoroutine());
	}

	 IEnumerator CountdownCoroutine (){

		Debug.Log ("Here??");

		float remainingTime = countdownTime;
		int floorTime = Mathf.FloorToInt(remainingTime);

		Debug.Log ("Remaining Time: " + remainingTime + " Floor Time: " + floorTime);
		while (remainingTime > 0)
		{
			yield return null;

			remainingTime -= Time.deltaTime;
			countdownUI.text = Mathf.Round (remainingTime).ToString ();
			int newFloorTime = Mathf.FloorToInt(remainingTime);

		}
			
		SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
	}

}
