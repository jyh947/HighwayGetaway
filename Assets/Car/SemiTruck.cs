using UnityEngine;
using System.Collections;

public class SemiTruck : BaseCar {

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		//		// Generate random numbers about where to start
		//		int startingLane = (int)Random.Range (0, RoadManager.numLanes - 1);
		//		//int startingLane = 1;
		//		transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY,
		//		                                  CopyCat.getDistanceTraveled() + 45f);
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update ();
		//		if (transform.position.z < CopyCat.getDistanceTraveled() - 20f) {
		//			//destroy car here
		//		}
	}
}

