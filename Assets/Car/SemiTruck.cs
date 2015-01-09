using UnityEngine;
using System.Collections;

public class SemiTruck : BaseCar {
	
	// Use this for initialization
	override protected void Start () {
		base.Start ();
		int startingLane;
		minSpeed = Globals.SemiTruckMinSpeed;
		maxSpeed = Globals.SemiTruckMaxSpeed;
		speed = Random.Range (minSpeed, maxSpeed);
		if (CarManager.toSpawn.Count > 0) {
			startingLane = CarManager.toSpawn [0];
			CarManager.toSpawn.RemoveAt (0);
			transform.position = new Vector3 (startX + startingLane * laneSwitchOffset, startY,
			                                  PlayerCar.getDistanceTraveled() + 45f);
		}
		//int startingLane = 1;
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update ();
		//		if (transform.position.z < CopyCat.getDistanceTraveled() - 20f) {
		//			//destroy car here
		//		}
		transform.Translate(0f, 0f, speed * Time.deltaTime);
	}
}