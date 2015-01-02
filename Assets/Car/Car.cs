using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float startX = 0f;
	public float startY = 0f;
	public float laneSwitchOffset = 3.375f;
	public int lowSpeed = 0;
	public Transform target;
	public float fract;
	public static float speed = Globals.StartingVelocity;
	public static float distanceTraveled;

	// Use this for initialization
	void Start () {
		// Generate random numbers about where to start
		// int startingLane = (int)Random.Range (0, RoadManager.numLanes);
		speed = Globals.StartingVelocity;
		int startingLane = 2;
		transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY, 0f);
		distanceTraveled = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		distanceTraveled = transform.position.z;
		transform.position = Vector3.Lerp (transform.position, target.position, fract);
		if (speed < Globals.LosingVelocity) {
			lowSpeed++;
		} else {
			lowSpeed = 0;
		}
		if (lowSpeed > 500) {
			Application.LoadLevel("GameOver");
		}
		if (Input.GetKeyDown ("up")) {
			if(speed < Globals.MaxVelocity){
				speed += Globals.SwipeVelocityChange;
			}
		} else if (Input.GetKeyDown ("down")) {
			if(speed > Globals.MinVelocity){
				speed -= Globals.SwipeVelocityChange;
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

	public static void setSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	public static float getDistanceTraveled()
	{
		return distanceTraveled;
	}

	public Transform getTransform()
	{
		return transform;
	}
}
