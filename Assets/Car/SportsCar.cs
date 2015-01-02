using UnityEngine;
using System.Collections;

public class SportsCar : MonoBehaviour {

	public float startX = 0f;
	public float startY = 0f;
	public float laneSwitchOffset = 3.375f;
	public static float speed = 7f;

	// Use this for initialization
	void Start () {
		// Generate random numbers about where to start
		int startingLane = (int)Random.Range (0, RoadManager.numLanes - 1);
		//int startingLane = 1;
		transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY,
		                                  CopyCat.getDistanceTraveled() + 45f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		if (transform.position.z < CopyCat.getDistanceTraveled() - 20f) {
			//destroy car here
		}
	}
}

