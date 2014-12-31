using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
	
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
