using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallScript : NetworkBehaviour 
{
	public Text countText;
	public Text golfText;
	public Text winText;
	public Text winTextR;
	private int count;
	private double golfCount;
	private int[] par = { 3, 4, 5 };
	// Use this for initialization
	void Start () 
	{
		count = 1;
		countText.text = "Par 3";
		golfCount = 0;
		setGolfText ();
		winText.text = "";
		winTextR.text = winText.text;
	}
	
	// Update is called once per frame
	void Update () {}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
			count++;
			if (count >= 2)
				setScore ();
		} 
	}
	void setScore ()
	{
		if (golfCount == 1)
			winText.text = "Hole in one!!!!!";
		else if ((int)golfCount - par[count - 2] == -3)
			winText.text = "Albatross!!!!";
		else if ((int)golfCount - par[count - 2] == -2)
			winText.text = "Eagle!!!";
		else if ((int)golfCount - par[count - 2] == -1)
			winText.text = "Birdie!!";
		else if ((int)golfCount - par[count - 2] == 0)
			winText.text = "Par!";
		else if ((int)golfCount - par[count - 2] == 1)
			winText.text = "Bogey";
		else if ((int)golfCount - par[count - 2] == 2)
			winText.text = "Double Bogey";
		else if ((int)golfCount - par[count - 2] == 3)
			winText.text = "Triple Bogey";
		else if ((int)golfCount - par[count - 2] > 3)
			winText.text = ((int)golfCount - par[count - 2]) + " over par :(";
		winTextR.text = winText.text;
	}
	void setGolfText()
	{
		golfText.text = "Count: " + golfCount.ToString();	
	}
	void OnCollisionEnter(Collision c)
	{
		if (!isLocalPlayer)
			return;
		if (c.gameObject.CompareTag("Player")) 
		{
			
			golfCount += 1;
			setGolfText();
		}
	}
}
