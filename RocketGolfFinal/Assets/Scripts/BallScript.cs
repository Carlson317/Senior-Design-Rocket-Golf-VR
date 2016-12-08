using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {
	public Text countText;
	public Text golfText;
	public Text winText;
	private int count, golfCount;
	private int[] par = { 3, 4, 5 };
	// Use this for initialization
	void Start () {
		count = 1;
		countText.text = "Par 3";
		golfCount = 0;
		setGolfText ();
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count++;
			if (count >= 2) {
				setScore ();
			}
		} 
	}
	void setScore (){
		if (golfCount == 1)
			winText.text = "Hole in one!!!!!";
		else if (golfCount - par[count - 2] == -3)
			winText.text = "Albatross!!!!";
		else if (golfCount - par[count - 2] == -2)
			winText.text = "Eagle!!!";
		else if (golfCount - par[count - 2] == -1)
			winText.text = "Birdie!!";
		else if (golfCount - par[count - 2] == 0)
			winText.text = "Par!";
		else if (golfCount - par[count - 2] == 1)
			winText.text = "Bogey";
		else if (golfCount - par[count - 2] == 2)
			winText.text = "Double Bogey";
		else if (golfCount - par[count - 2] == 3)
			winText.text = "Triple Bogey";
		else if (golfCount - par[count - 2] > 3)
			winText.text = (golfCount - par[count - 2]) + " over par :(";
	}
	void setGolfText(){
		golfText.text = "Count: " + golfCount.ToString();	
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag("Player")) {
			golfCount++;
			setGolfText ();
		}
	}
}
