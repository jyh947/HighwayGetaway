using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For lists and dictionaries


public class Lane : MonoBehaviour {
	
	public static LaneManager laneManager;
	public List<NonPlayerCar> cars = new List<NonPlayerCar>();  // public for debugging

	public void addCarToLane( NonPlayerCar newCar ) {
		if ( !cars.Contains (newCar) ) {
			cars.Add( newCar );
		}
		else {
			// Car is already in lane. idk what.
			Debug.LogError("Car already in lane.");
		}
	}

	// Use this for initialization
	void Start () {
		cars = new List<NonPlayerCar>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void checkIfCarsOffscreen() {
		foreach( NonPlayerCar thisCar in cars ) {
			//if( thisCar.position > laneManager.MAXIMUM_CAR_DISTANCE ) {
				thisCar.stopCar();
				cars.Remove( thisCar );
			//}
		}
	}

}
