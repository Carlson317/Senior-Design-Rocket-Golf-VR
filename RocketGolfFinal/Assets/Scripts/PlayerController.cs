using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public Text golfText;
	public Text winText;
	private int count, golfCount;
	private Rigidbody myRB;
	public Transform move;
	private Vector2 touchOrigin = -Vector2.one;
	void Start(){
		myRB = GetComponent<Rigidbody> ();
		count = 0;
		golfCount = 0;
		setText ();
		setGolfText ();
		winText.text = "";

	}
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		float moveHorizontal= 0.0f;
		float moveVertical = 0.0f;
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
			 moveHorizontal = Input.GetAxis ("Horizontal");
			 moveVertical = Input.GetAxis ("Vertical");
		#elif UNITY_ANDROID
			//Check if Input has registered more than zero touches
			if (Input.touchCount > 0)
			{
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}

			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (myTouch.phase == TouchPhase.Moved && touchOrigin.x >= 0)
			{
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;

				//Calculate the difference between the beginning and end of the touch on the x axis.
					float x = touchEnd.x - touchOrigin.x;

				//Calculate the difference between the beginning and end of the touch on the y axis.
					float y = touchEnd.y - touchOrigin.y;

				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				//touchOrigin.x = -1;
				
					moveHorizontal = x > 0 ? 1 : -1;
					moveVertical = y > 0 ? 1 : -1;
					//touchOrigin = touchEnd;
				}
				else if (myTouch.phase == TouchPhase.Ended)
				{
					touchOrigin.x = -1;
				}
			}

		#endif //End of mobile platform dependendent compilation section started above with #elif
		//Check if we have a non-zero value for horizontal or vertical

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		myRB.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count++;
			if (count >= 10) {
				winText.text = "YOU WIN!!!!!";
			}
			setText ();
		} 
	}
	void setText(){
		countText.text = "Count: " + count.ToString();	
	}
	void setGolfText(){
		golfText.text = "Count: " + golfCount.ToString();	
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag("GolfBall")) {
			golfCount++;
			setGolfText ();

		}
	}
}
