using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallScript : MonoBehaviour 
{
	public TextMesh winText;
	private double golfCount;
	private int par = 15;
	public static bool GOAL = false;
	// Use this for initialization
	IEnumerator Start () 
	{
		golfCount = 0;
		winText.text = "";
		yield return new WaitUntil (mapGenerated);
		this.transform.position = new Vector3(Random.Range(-50,50), 0, Random.Range(-50,50));
		RaycastHit beam;
		float heightAboveGround = 0f;
		if (Physics.Raycast (this.transform.position, Vector3.down ,out beam, Mathf.Infinity))
		{
			heightAboveGround = beam.distance;
			Debug.Log ("Hole in the air " + heightAboveGround);
			//Debug.Log (Vector3.down.ToString());
		}
		this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y - heightAboveGround +2.5f), this.transform.position.z);
	}

	void Update(){
		if (this.transform.position.y < -200) {
			this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
			setScore ();
			GOAL = true;
		} 
	}
	void setScore ()
	{
		if (golfCount == 1)
			winText.text = "Hole in one!";
		else if ((int)golfCount - par < -3)
			winText.text = (par - (int)golfCount) + " under par!";
		else if ((int)golfCount - par == -3)
			winText.text = "Albatross!";
		else if ((int)golfCount - par == -2)
			winText.text = "Eagle!";
		else if ((int)golfCount - par == -1)
			winText.text = "Birdie!";
		else if ((int)golfCount - par == 0)
			winText.text = "Par";
		else if ((int)golfCount - par == 1)
			winText.text = "Bogey";
		else if ((int)golfCount - par == 2)
			winText.text = "Double Bogey";
		else if ((int)golfCount - par == 3)
			winText.text = "Triple Bogey";
		else if ((int)golfCount - par > 3)
			winText.text = ((int)golfCount - par) + " over par :(";
	}
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag("Player")) 
		{
			golfCount += .5;
		}
	}
	bool checkGoalMade(){
		return GOAL;
	}
	bool mapGenerated()
	{
		return(MapGenerator.mapMade || SingleMapGenerator.mapMade);
	}
}
