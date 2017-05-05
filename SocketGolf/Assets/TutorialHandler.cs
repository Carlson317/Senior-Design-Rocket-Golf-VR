using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour {

	GameObject BeanGuy;
	GameObject GolfBall;
	public GameObject Target;


	Vector3 ballInitPos;
	Vector3 playerInitPos;
	Rigidbody playerRB, golfBallRB;

	private Text UIText;
	List<string> textList = new List<string> (){
		"Welcome to the SocketGolf Tutorial",
		"Step 1: Hitting the Ball\nHit the ball in front of you.", "Nice Work!",
		"Step 2: Changing Trajectory\nThe Angle of Flight can be changed by pressing 'RB' and 'LB'","Change to angle 0 to putt the ball", "Excellent!", 
		"Step 3: Similarily you can change the power by using 'RT' and 'LT'", "Change to Max Power and the Max Angle and hit the ball", "Nice Job!",
		"Step 4: Jumping\nJump with 'Y'", "You nailed it!",
		"Step 5: Take aim and hit the target!", "You are ready"};

	private int displayText = 0;
	private bool[] stepsCompleted = { false, false, false, false, false };

	public TutorialController tutorialController;

	void Start(){
		BeanGuy = GameObject.FindGameObjectWithTag ("Player");
		GolfBall = GameObject.FindGameObjectWithTag ("Kickball");
		playerRB = BeanGuy.GetComponent<Rigidbody> ();
		golfBallRB = GolfBall.GetComponent<Rigidbody> ();
		playerInitPos = BeanGuy.transform.position;
		ballInitPos = GolfBall.transform.position;

		UIText = GameObject.Find ("IntroText").GetComponent<Text> ();
		UIText.text = textList [displayText];
		StartCoroutine (Pause5 ());
	}

	void Update(){
		//Check and see if ball or player has fallen off
		if(BeanGuy.transform.position.y < 0 || GolfBall.transform.position.y < 0){
			ResetPositions ();
		}
		//Wait for the Jump in step 4
		if(tutorialController.jumpInput > 0 && Physics.Raycast (transform.position, Vector3.down, (tutorialController.moveSetting.distToGround * 2), 
			tutorialController.moveSetting.ground) && stepsCompleted[2] == true && stepsCompleted[3] == false){
			//Jumped for step 4
			stepsCompleted[3] = true;
			StartCoroutine (RewardText ());
			StartCoroutine (SetUpNextStep ());
		}

		//check to see if the end condition is met
		if(UIText.text == "Step 5: Take aim and hit the target!\nBall hit the Outer Ring!" ||
			UIText.text == "Step 5: Take aim and hit the target!\nBall hit the Inner Ring!" ||
			UIText.text == "Step 5: Take aim and hit the target!\nBall hit the Bullseye!"){

			//Target was hit
			StartCoroutine(RewardText());
			ReturnToMainMenu ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Kickball" && stepsCompleted [0] == false) {
			//Drove the ball step 1 is done
			stepsCompleted[0] = true;
			StartCoroutine (RewardText ());
			StartCoroutine (SetUpNextStep ());
		}
		if (col.gameObject.tag == "Kickball" && tutorialController.bumperclicks == 0 && stepsCompleted [0] == true && stepsCompleted [1] == false) {
			//Ball has been putted
			stepsCompleted[1] = true;
			StartCoroutine (RewardText ());
			StartCoroutine (SetUpNextStep ());
		}
		if (col.gameObject.tag == "Kickball" && tutorialController.triggerclicks == 2 && tutorialController.bumperclicks == 2 && stepsCompleted [1] == true && stepsCompleted [2] == false) {
			// Ultimate hit
			stepsCompleted[2] = true;
			StartCoroutine (RewardText ());
			StartCoroutine (SetUpNextStep ());
		}
	}

	void ResetPositions(){
		BeanGuy.transform.rotation = Quaternion.identity;
		BeanGuy.transform.position = playerInitPos;
		GolfBall.transform.position = ballInitPos;
		golfBallRB.angularVelocity = Vector3.zero;
		golfBallRB.velocity = Vector3.zero;
	}
	IEnumerator RewardText(){
		displayText++;
		UIText.text = textList [displayText];
		yield return new WaitForSecondsRealtime (3.0f);
	}
	IEnumerator SetUpNextStep(){
		//Need to reset position of player and ball
		yield return new WaitForSecondsRealtime(3.0f);
		ResetPositions();
		displayText++;
		UIText.text = textList [displayText];
		//Step 2 and 3 require an additional step
		if (UIText.text == "Step 2: Changing Trajectory\nThe Angle of Flight can be changed by pressing 'RB' and 'LB'") {
			StartCoroutine (Pause5 ());
		} else if (UIText.text == "Step 3: Similarily you can change the power by using 'RT' and 'LT'") {
			StartCoroutine (Pause5 ());
		}

		if (stepsCompleted [3] == true) {
			Target.SetActive (true);
		}
	}
	IEnumerator Pause5(){
		BeanGuy.GetComponent<TutorialController> ().enabled = false;
		yield return new WaitForSecondsRealtime (5.0f);
		displayText++;
		UIText.text = textList [displayText];
		BeanGuy.GetComponent<TutorialController> ().enabled = true;
	}

	void ReturnToMainMenu(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

	}
}
