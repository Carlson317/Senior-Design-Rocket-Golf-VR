using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	private Text UIText;
	public GameObject PauseMenu;
	public TextMesh angle;
	public TextMesh power;

	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 12;
		public float rotateVel = 100;
		public float jumpVel = 7;
		public float distToGround = 1.1f;
		public LayerMask ground;
	}
	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel = 0.25f;
	}
	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = .1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
	}

	public MoveSettings moveSetting = new MoveSettings ();
	public PhysSettings physSetting = new PhysSettings ();
	public InputSettings inputSetting = new InputSettings ();

	public float inputDelay = .1f;
	public float forwardVel = 12;
	public float rotateVel = 100;
	public float kickpower;

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	public float forwardInput, turnInput, jumpInput;

	public  int triggerclicks = 1;
	public int bumperclicks = 2;
	private string[] Powers = { "Low", "Normal", "High" };
	private float upward = 250f;
	private float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 5.0f;

	public Quaternion TargetRotation{
		get { return targetRotation;}
	}

	bool Grounded(){
		return Physics.Raycast (transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground);
	}
	void Start(){
		UIText = GameObject.Find ("IntroText").GetComponent<Text> ();
		targetRotation = transform.rotation;
		rBody = GetComponent<Rigidbody> ();
		forwardInput = turnInput = jumpInput =  0;
	}

	void GetInput(){
		forwardInput = Input.GetAxis (inputSetting.FORWARD_AXIS);
		turnInput = Input.GetAxis (inputSetting.TURN_AXIS);
		jumpInput = Input.GetAxisRaw (inputSetting.JUMP_AXIS);
	}

	void Update(){
		GetInput ();
		Turn ();

		if (Input.GetKeyDown (KeyCode.JoystickButton0)) {
			Debug.Log ("X???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
			Debug.Log ("A???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
			Debug.Log ("B??"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton3)) {
			Debug.Log ("Y???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton4) || Input.GetKeyDown ("z")) {
			Debug.Log ("LB???"); 
			bumperclicks--;
			if (bumperclicks <= -1)
				bumperclicks = 2;
			Debug.Log ("bumpberclicks: " + bumperclicks.ToString ());
			upward = 0f + 125f * (float)bumperclicks;
			angle.text = "Angle:" + (Powers[bumperclicks]) + " " + upward.ToString();
		} else if (Input.GetKeyDown (KeyCode.JoystickButton5) || Input.GetKeyDown ("x")) {
			Debug.Log ("RB???");
			bumperclicks++;
			if (bumperclicks >= 3)
				bumperclicks = 0;
			Debug.Log ("bumpberclicks: " + bumperclicks.ToString ());
			upward = 0f + 125f * (float)bumperclicks;
			power.text = "Power:" + (Powers[triggerclicks]) + " " + kickpower.ToString();
		} else if (Input.GetKeyDown (KeyCode.JoystickButton6) || Input.GetKeyDown ("q")) {
			Debug.Log ("LT???"); 
			triggerclicks--;
			if (triggerclicks <= -1)
				triggerclicks = 2;
			Debug.Log ("triggerclicks: " + triggerclicks.ToString ());
			kickpower = 5f + 5f * (float)triggerclicks;
			string phrase = "Power:" + (Powers [triggerclicks]) + " " + kickpower.ToString ();
			power.text = "Power:" + (Powers[triggerclicks]) + " " + kickpower.ToString();
			executedTime = Time.time;
		} else if (Input.GetKeyDown (KeyCode.JoystickButton7) || Input.GetKeyDown ("e")) {
			Debug.Log ("RT???"); 
			triggerclicks++;
			if (triggerclicks >= 3)
				triggerclicks = 0;
			Debug.Log ("triggerclicks: " + triggerclicks.ToString ());
			kickpower = 5f + 5f * (float)triggerclicks;
			string phrase = "Power:" + (Powers [triggerclicks]) + " " + kickpower.ToString ();
			power.text = "Power:" + (Powers[triggerclicks]) + " " + kickpower.ToString();
			executedTime = Time.time;

		} else if (Input.GetKeyDown (KeyCode.JoystickButton8)) {
			Debug.Log ("BACK???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton9)|| Input.GetKeyDown ("l")) {
			Debug.Log ("START???"); 
			if (PauseMenu.activeSelf == true) {
				PauseMenu.SetActive (false);
			} else {
				PauseMenu.SetActive (true);
			}
		} else if (Input.GetKeyDown (KeyCode.JoystickButton10)) {
			Debug.Log ("left stick???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton11)) {
			Debug.Log ("right stick???"); 
		}
	}


	void FixedUpdate(){
		Run ();
		Jump ();
		rBody.velocity = transform.TransformDirection (velocity);


	}

	void Run(){
		if (Mathf.Abs (forwardInput) > inputSetting.inputDelay) {
			//move
			velocity.z = moveSetting.forwardVel * forwardInput;
		} else {
			velocity.z = 0;
		}
	}

	void Turn(){

		targetRotation *= Quaternion.AngleAxis (moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
		transform.rotation = targetRotation;
	}

	void Jump(){

		if (jumpInput > 0 && Physics.Raycast (transform.position, Vector3.down, (moveSetting.distToGround * 2), moveSetting.ground)) {
			//jump
			velocity.y = moveSetting.jumpVel;

		} else if (jumpInput == 0 && Grounded ()) {
			//zero out velocity.y
			velocity.y = 0;
		} else {
			//decrese velocity.y
			velocity.y -= physSetting.downAccel;
		}
	}
//	//Caleb wrote this if it breaks stuff you can blame him! :P
	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("Collision with: " + col.transform.name);
		if(col.gameObject.tag == "Kickball")
		{
			float Xdir = (col.transform.position.x - this.transform.position.x)*100;
			float Zdir = (col.transform.position.z - this.transform.position.z)*100;
			Vector3 dir = new Vector3 (Xdir, upward, Zdir);
			dir = dir.normalized * kickpower;
			Rigidbody r = col.gameObject.GetComponent<Rigidbody>();
			//r.AddForce (new Vector3 (0.0f, upward, 0.0f));
			r.AddForce (dir, ForceMode.Impulse);
		}
	}
}
