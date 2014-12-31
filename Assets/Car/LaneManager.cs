using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneManager : MonoBehaviour {

	// THERE SHOULD BE ONLY LANEMANAGER IN A SCENE

	public float LANE_LENGTH = 100f; // otherwise known as the length of the lane (cars are removed if
		// they go beyond the scope of the camera.
	public float MINIMUM_CAR_SPAWN_DISTANCE = 10f;
	public float carSpawnFrequency = 0;

	public static List<GameObject> carTypes = new List<GameObject>(); // List of all the possible types of cars
	public List<Lane> lanes = new List<Lane>(); // Public so that it can be instantitated

	private List<NonPlayerCar> cars = new List<NonPlayerCar>();

	public void makeNewCar() {
		if( carTypes.Count == 0 ) {
			Debug.LogError("LaneManager: No cars in list of types of cars.");
			return;
		} else if( lanes.Count == 0 ) {
			Debug.LogError("LaneManager: No lanes in list of lanes.");
			return;
		}
		int newCarType = (int) Random.Range( 0, carTypes.Count - 1 );
		int newCarLane = (int) Random.Range( 0, lanes.Count - 1 );
		NonPlayerCar newCar = lanes[newCarLane].makeNewCarInLane( carTypes[ newCarType ] );
		cars.Add( newCar );
		Debug.Log( "Created new car of type " + newCarType + " in lane " + newCarLane + "." );
	}

	public void removeCar( NonPlayerCar targetCar, Lane targetLane ) {
		cars.Remove( targetCar );
		targetLane.cars.Remove( targetCar );
		Destroy( targetCar );
	}

	// Use this for initialization
	void Start () {
		StartCoroutine( spawnCarsAtInterval( carSpawnFrequency ) );
	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator spawnCarsAtInterval( float spawnFrequency ) {
		while( true ) {
			makeNewCar();
			yield return new WaitForSeconds( spawnFrequency );
		}
	}
	
}
