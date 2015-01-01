using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float startX = 2.375f;
	public float startY = 0.5f;
	public float laneSwitchOffset = 3.375f;
	public float speed = 5f;

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

		// Input handling
		if (Input.GetKeyDown ("left")) {
			transform.Translate (-laneSwitchOffset, 0f, 0f);
		} else if (Input.GetKeyDown ("right")) {
			transform.Translate (laneSwitchOffset, 0f, 0f);
		}
	}

	public float getSpeed()
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
