using UnityEngine;
using System.Collections.Generic;

public class CarManager : MonoBehaviour {

	public BaseCar carType1; //SportsCar
	public BaseCar carType2; //MiniVan
	public BaseCar carType3; //SemiTruck

	public int numCars;

	private static List<BaseCar> carTypes = new List<BaseCar>();
	private static List<BaseCar> carList = new List<BaseCar>();

	// Use this for initialization
	void Start () {
		carTypes = new List<BaseCar>(3);
		carList = new List<BaseCar>(numCars);

		// Add different types to list
		carTypes.Add (carType1);
		carTypes.Add (carType2);
		carTypes.Add (carType3);

		// Generate random cars based on type list
		int type = 0;
		for (int i = 0; i < numCars; ++i) {
			type = (int)Random.Range(0, carTypes.Count);
			carList.Add ((BaseCar)Instantiate (carTypes[type]));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void cleanCars() {

	}
}
