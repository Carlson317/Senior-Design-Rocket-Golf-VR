using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetScript : MonoBehaviour {

	private Text UIText;
	private int HitCount;
	// Use this for initialization
	void Start () {
		UIText = GameObject.Find ("IntroText").GetComponent<Text> ();
		HitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){

		//TODO need to figure out why this doesn't break and it still reconginizes multiple contacts on target
			if (col.transform.tag == "Kickball" && this.gameObject.name == "Bullseye") {
				UIText.text += "\nBall hit the Bullseye!";
				HitCount++;
				Debug.Log ("Hit Count: " + HitCount);
			}else if (col.transform.tag == "Kickball" && this.gameObject.name == "InnerRing") {
				UIText.text += "\nBall hit the Inner Ring!";
				HitCount++;
				Debug.Log ("Hit Count: " + HitCount);
			}else if (col.transform.tag == "Kickball" && this.gameObject.name == "OuterRing") {
				UIText.text += "\nBall hit the Outer Ring!";
				HitCount++;
				Debug.Log ("Hit Count: " + HitCount);
			}

	}
}
