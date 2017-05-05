using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour {
	public float speed;
	public float kickpower;
	private Rigidbody rb;
	private float sprint;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		sprint = 1.0f;
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.JoystickButton4)) {
			Debug.Log ("LB???");
		}
	}
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton0)) {//A
			Debug.Log ("A???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton1)) {//B
			Debug.Log ("B???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton2)) {//X
			Debug.Log ("X???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton3)) {
			Debug.Log ("Y???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton4)) {
			Debug.Log ("LB???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton5)) {
			Debug.Log ("RB???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton6)) {
			Debug.Log ("LT???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton7)) {
			Debug.Log ("RT???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton8)) {//left stick click
			Debug.Log ("left stick click???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton9)) {//right stick click
			Debug.Log ("right stick click???"); 
		}else if (Input.GetKeyDown (KeyCode.JoystickButton10)) {//start
			Debug.Log ("start???"); 
		} else if (Input.GetKeyDown (KeyCode.JoystickButton11)) {//select
			Debug.Log ("select???"); 
		}
		float sprint = 1.0f;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (Input.GetKeyDown (KeyCode.JoystickButton4)) {
			sprint = 10.0f;
		} else if (Input.GetKeyUp (KeyCode.JoystickButton4)) {
			sprint = 1.0f;
		}
		transform.Rotate (new Vector3(0.0f, moveHorizontal * 2.0f,0.0f));
		rb.AddForce (transform.forward * speed * moveVertical * sprint);

	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Kickball")
		{
			float upward = 250.0f;
			float Xdir = (col.transform.position.x - this.transform.position.x)*100;
			float Zdir = (col.transform.position.z - this.transform.position.z)*100;
			if(Input.GetButton("b"))
			{
				upward = 0.0f;
			}
			Vector3 dir = new Vector3 (Xdir, upward, Zdir);
			dir = dir.normalized * kickpower;
			Rigidbody r = col.gameObject.GetComponent<Rigidbody>();
			//r.AddForce (new Vector3 (0.0f, upward, 0.0f));
			r.AddForce (dir, ForceMode.Impulse);
		}
	}

}
