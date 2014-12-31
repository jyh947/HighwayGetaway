using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float laneSwitchOffset = 3.375f;
	
	private static float speed;
	private static float distanceTravelled;

	// Use this for initialization
	void Start () {
		speed = 5f;
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
