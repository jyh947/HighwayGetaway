using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneManager : MonoBehaviour {

	public static float MAXIMUM_CAR_DISTANCE = 100f;
	public static float MINIMUM_CAR_SPAWN_DISTANCE = 10f;
	public float carSpawnFrequency = 0;
	
	public List<Lane> lanes = new List<Lane>();
	public List<NonPlayerCar> cars = new List<NonPlayerCar>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
