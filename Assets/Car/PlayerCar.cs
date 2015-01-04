using UnityEngine;
using System.Collections;

public class PlayerCar : BaseCar {

	// Input related vars
	public float minSwipeDistX;
	public float minSwipeDistY;

	public int lowSpeedCounter = 0;

	public static float distanceTraveled;
	public static float playerSpeed;

	override protected void Start() {
		base.Start ();
		playerSpeed = 0f;
		distanceTraveled = 0f;

	}

	override protected void Update() {

		handleInputs ();
		
		distanceTraveled = transform.position.z;
		playerSpeed = speed;
		//transform.Translate(0f, 0f, speed * Time.deltaTime);

		base.Update ();

		// Check GameOver state
		if (speed < Globals.LosingVelocity) {
			lowSpeedCounter++;
		} else {
			lowSpeedCounter = 0;
		}
		
		if (lowSpeedCounter > 500) { // i think something else should be responsible for this but this is fine for now
			Application.LoadLevel("GameOver");
		}
	}

	private void handleInputs() {
		Vector3 startPos = new Vector3();
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

}