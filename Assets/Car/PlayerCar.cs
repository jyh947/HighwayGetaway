using UnityEngine;
using System.Collections;

public class PlayerCar : BaseCar {

	// Input related vars
	public float minSwipeDistX = 50f;
	public float minSwipeDistY = 50f;

	public int lowSpeedCounter = 0;
	public float timeOfSlow = 0f;

	public static float distanceTraveled = 0f;
	public static float playerSpeed = 0f;
	
	public Vector2 startPos;

	override protected void Start() {
		base.Start ();
		Globals.GameOver = false;
		speed = Globals.StartingVelocity;
		playerSpeed = 0f;
		distanceTraveled = 0f;
		Physics.gravity = new Vector3(0, 0, 0);
	}

	override protected void Update() {

		if (!Globals.GameOver) {
			handleInputs ();
			distanceTraveled = transform.position.z;
			playerSpeed = speed;
			
			//transform.Translate(0f, 0f, speed * Time.deltaTime);
			
			base.Update ();
			
			// Check GameOver state
			if (speed < Globals.LosingVelocity) {
				if ((Time.time - timeOfSlow) > Globals.LosingSeconds) { // i think something else should be responsible for this but this is fine for now
					Globals.GameOver = true;
					speed = 0;
					Physics.gravity = new Vector3(0, -9.81F, 0);
					GUIManager.GameOver();
				}
			} else {
				timeOfSlow = Time.time;
			}
		}
	}

	private void handleInputs() {
		if (Input.GetKeyDown ("up")) {
			moveUp();
		} else if (Input.GetKeyDown ("down")) {
			moveDown();
		} else if (Input.GetKeyDown ("left")) {
			moveLeft();
		} else if (Input.GetKeyDown ("right")) {
			moveRight();
		} 
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			switch (touch.phase)
			{
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				if(startPos.x == 0 &&  startPos.y == 0){
					startPos.x = touch.position.x;
					startPos.y = touch.position.y;
				}
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY)
				{
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					if (swipeValue > 0){
						moveUp();
					}
					else if (swipeValue < 0){
						moveDown();
					}
				}
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > minSwipeDistX)
				{
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					if (swipeValue < 0){
						moveLeft();
					}
					else if (swipeValue > 0){
						print ("touch.position.x = " + touch.position.x);
						print ("startPos.x = " + startPos.x);
						print ("swipeDistHorizontal = " + swipeDistHorizontal);
						print ("swipeValue = " + swipeValue);
						print ("WHY IS THIS HAPPENING");
						moveRight();
					}
				}
				break;
			}
		}
		setSpeed (speed);
	}
	
	public int getLowSpeedCounter()
	{
		return lowSpeedCounter;
	}

	public static float getDistanceTraveled()
	{
		return distanceTraveled;
	}

	public static float getSpeed() {
		return playerSpeed;
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.name == "MiniVan" || collisionInfo.collider.name == "SemiTruck"
		    || collisionInfo.collider.name == "SportsCar") {
			print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
			print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
			print("Their relative velocity is " + collisionInfo.relativeVelocity);
			Globals.GameOver = true;
			speed = 0;
			Physics.gravity = new Vector3(0, -9.81F, 0);
			GUIManager.GameOver();
			//Application.LoadLevel("GameOver");
		}
	}

	//Application.LoadLevel ("GameOver");

}