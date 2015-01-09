using UnityEngine;
using System.Collections.Generic;

public class CarManager : MonoBehaviour {
	
	public BaseCar carType1; //SportsCar
	public BaseCar carType2; //MiniVan
	public BaseCar carType3; //SemiTruck
	public static List<int> toSpawn = new List<int>();
	
	public int numCars;
	
	private static List<BaseCar> carTypes = new List<BaseCar>();
	private static List<BaseCar> carList = new List<BaseCar>();

	private float timeSinceSpawn;
	
	// Use this for initialization
	void Start () {
		carTypes = new List<BaseCar>(3);
		carList = new List<BaseCar>(numCars);
		
		// Add different types to list
		carTypes.Add (carType1);
		carTypes.Add (carType1);
		carTypes.Add (carType2);
		carTypes.Add (carType3);

		timeSinceSpawn = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if((Time.time - timeSinceSpawn) > 5f) {
			for(int i = -1; i < RoadManager.numLanes - 1; i++){
				toSpawn.Add(i);
			}
			while((carList.Count < 20) && (toSpawn.Count > 0)){
				int type = 2;
				//type = (int)Random.Range(0, carTypes.Count - 1);
				BaseCar newCar = (BaseCar)Instantiate (carTypes[type]);
				carList.Add(newCar);
				print(carList.Count);
			}
			if(carList.Count >=18) {
				for (int i = 0; i < carList.Count; ++i) {
					if( carList[i].getTransform().position.z < PlayerCar.getDistanceTraveled() - 20f) {
						Destroy(carList[i], 1.0f);
						carList.RemoveAt(i);
						//i--;
					}
				}
			}
			timeSinceSpawn = Time.time;
		}
	}

	void cleanCars() {
		
	}
}
