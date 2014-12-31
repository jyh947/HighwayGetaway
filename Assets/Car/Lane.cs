using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For lists and dictionaries


public class Lane : MonoBehaviour {
	
	public static LaneManager laneManager;
	public List<NonPlayerCar> cars = new List<NonPlayerCar>();  // public for debugging

	private Vector3 startOfLane;

	public NonPlayerCar makeNewCarInLane( GameObject newCarType ) {
		NonPlayerCar newCar = (NonPlayerCar) Instantiate( newCarType, transform.position, Quaternion.identity );
		cars.Add( newCar );
		return newCar;

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		checkIfCarsOffscreen();
	}

	private void checkIfCarsOffscreen() {
		foreach( NonPlayerCar thisCar in cars ) {
			if( thisCar.transform.localPosition.z > laneManager.LANE_LENGTH ) {
				laneManager.removeCar( thisCar, this );
			}
		}
	}

}
