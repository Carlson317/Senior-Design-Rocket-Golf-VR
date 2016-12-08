using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		public float speed;
		public Text countText;
		public Text golfText;
		public Text winText;
		private int count, golfCount;
		private Vector2 touchOrigin = -Vector2.one;
		void Start(){
			count = 0;
			golfCount = 0;
			//setText ();
			//setGolfText ();
			winText.text = "";

		}
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
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
				else if ((myTouch.phase == TouchPhase.Moved || myTouch.phase == TouchPhase.Stationary) && touchOrigin.x >= 0)
				{
					//Set touchEnd to equal the position of this touch
					Vector2 touchEnd = myTouch.position;
					//Calculate the difference between the beginning and end of the touch on the x axis.
					//float x = touchEnd.x - touchOrigin.x;

					//Calculate the difference between the beginning and end of the touch on the y axis.
					//float y = touchEnd.y - touchOrigin.y;

					//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
					//touchOrigin.x = -1;

					//h = x > 0 ? 1 : -1;
					//v = y > 0 ? 1 : -1;
					if(touchEnd.x > (Screen.width * 0.66)){
						h = 1;
					}
					else if (touchEnd.x < (Screen.width * 0.33))
					{
						h = -1;
					}
					else
					{
						h = 0;
					}
					if(touchEnd.y >= (Screen.height * 0.5)){
						v = 1;
					}
					else
					{
						v = -1;
					}

					//touchOrigin = touchEnd;
				}
				else if (myTouch.phase == TouchPhase.Ended)
				{
					touchOrigin.x = -1;
				}
			}
            m_Car.Move(h, v, v, 0f);
#endif
        }
		/*void OnTriggerEnter(Collider other) {
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
		}*/
    }
}
