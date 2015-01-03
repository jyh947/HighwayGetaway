using UnityEngine;
using System.Collections;

public class CopyCat : MonoBehaviour {
	public float startX = 0f;
	public float startY = 0f;
	public float laneSwitchOffset = 3.375f;
	public Vector2 startPos;
	public float minSwipeDistX;
	public float minSwipeDistY;
	public static float speed = Globals.StartingVelocity;
	public static float distanceTraveled = 0f;
	public static Transform transformCopy;

	void Start () {
		speed = Globals.StartingVelocity;
		int startingLane = 2;
		transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY, 0f);
		distanceTraveled = 0f;
	}

	void Update () {
		transformCopy = transform;
		distanceTraveled = transform.position.z;
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		// Input handling
		
		if (Input.GetKeyDown ("up")) {
			moveUp();
		} else if (Input.GetKeyDown ("down")) {
			moveDown();
		}else if (Input.GetKeyDown ("left")) {
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
		Car.setSpeed (speed);
	}

	public static float getDistanceTraveled() {
		return distanceTraveled;
	}

	
	public static float getSpeed()
	{
		return speed;
	}

	public static Transform getTransform()
	{
		return transformCopy;
	}

	void moveUp()
	{
		if(speed < Globals.MaxVelocity){
			speed += Globals.SwipeVelocityChange;
		}
	}
	
	void moveDown()
	{
		if(speed > Globals.MinVelocity){
			speed -= Globals.SwipeVelocityChange;
		}
	}
	
	void moveLeft()
	{
		if (transform.position.x > Globals.LeftBorder) {
			transform.Translate (-laneSwitchOffset, 0f, 0f);
		}
	}
	
	void moveRight()
	{
		if (transform.position.x < Globals.RightBorder) {
			transform.Translate (laneSwitchOffset, 0f, 0f);
		}
	}
}
