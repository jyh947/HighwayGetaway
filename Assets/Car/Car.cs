using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float startX = 2.375f;
	public float startY = 0.5f;
	public float laneSwitchOffset = 3.375f;
	public float minSwipeDistX;
	public float minSwipeDistY;
	public int lowSpeed = 0;
	public Vector2 startPos;
	public static float speed = 8f;
	private static float distanceTravelled;

	// Use this for initialization
	void Start () {
		// Generate random numbers about where to start
		// int startingLane = (int)Random.Range (0, RoadManager.numLanes);
		int startingLane = 1;
		transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY, 0f);
		distanceTravelled = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		distanceTravelled = transform.localPosition.z;
		if (speed < 7f) {
			lowSpeed++;
		} else {
			lowSpeed = 0;
		}
		if (lowSpeed > 500) {
			Application.LoadLevel("GameOver");
		}
		// Input handling
		if (Input.GetKeyDown ("left")) {
			transform.Translate (-laneSwitchOffset, 0f, 0f);
		} else if (Input.GetKeyDown ("right")) {
			transform.Translate (laneSwitchOffset, 0f, 0f);
		} else if (Input.GetKeyDown ("up")) {
			if(speed < 14f){
				speed += 2f;
			}
		} else if (Input.GetKeyDown ("down")) {
			if(speed > 6f){
				speed -= 2f;
			}
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
						if(speed < 14f){
							speed += 2f;
						}
					}
					else if (swipeValue < 0){
						if(speed > 4f){
							speed -= 2f;
						}
					}
				}

				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > minSwipeDistX)	
				{
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					if (swipeValue < 0){
						transform.Translate (-laneSwitchOffset, 0f, 0f);
					}	
					else if (swipeValue > 0){
						transform.Translate (laneSwitchOffset, 0f, 0f);
					}
				}
				break;
			}
		}
	}
	
	public int getLowSpeed()
	{
		return lowSpeed;
	}

	public static float getSpeed()
	{
		return speed;
	}

	public static float getDistanceTravelled()
	{
		return distanceTravelled;
	}

	public Transform getTransform()
	{
		return transform;
	}
}
