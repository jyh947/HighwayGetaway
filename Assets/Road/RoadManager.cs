using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {
	
	public Transform leftBorderPrefab;
	public Transform rightBorderPrefab;
	public Transform dividerPrefab;
	public Transform roadTilePrefab;
	
	public static int minLanes = 5;
	public static int maxLanes = 5;

	public int maxRoadLength = 10;
	public int recycleOffset = 5;

	public int maxRoadTileObjects = 120;

	public float minChangeTime = 3f;
	public float maxChangeTime = 6f;
	
	public Vector3 originalStartPosition;
	private Vector3 startPosition;
	private Vector3 nextPosition;
	
	private Queue<Transform> leftBorderQueue;
	private Queue<Transform> rightBorderQueue;
	private Queue<Transform> dividerQueue;
	private Queue<Transform> roadTileQueue;

	private static float tileHeight;
	private static float borderWidth;
	private static float dividerWidth;
	private static float roadTileWidth;
	private static float tileOffset; // horizontal distance between two road/divider tiles

	public static int numLanes = 5;
	
	private float timeUntilRoadChange = 3f;

	// Use this for initialization
	void Start () {
		// Construct queues
		leftBorderQueue = new Queue<Transform> (maxRoadTileObjects);
		rightBorderQueue = new Queue<Transform> (maxRoadTileObjects);
		dividerQueue = new Queue<Transform> (maxRoadTileObjects);
		roadTileQueue = new Queue<Transform> (maxRoadTileObjects);

		// Generate road assets
		for (int i = 0; i < maxRoadTileObjects; ++i) {
			leftBorderQueue.Enqueue((Transform)Instantiate(leftBorderPrefab));
			rightBorderQueue.Enqueue((Transform)Instantiate(rightBorderPrefab));
			dividerQueue.Enqueue((Transform)Instantiate(dividerPrefab));
			roadTileQueue.Enqueue((Transform)Instantiate(roadTilePrefab));
		}

		// Get prefab dimensions
		Renderer r;
		
		r = leftBorderQueue.Peek().GetComponent<Renderer>();
		borderWidth = r.bounds.size.x;

		r = dividerQueue.Peek().GetComponent<Renderer>();
		dividerWidth = r.bounds.size.x;

		r = roadTileQueue.Peek().GetComponent<Renderer> ();
		roadTileWidth = r.bounds.size.x;

		tileHeight = r.bounds.size.z;
		tileOffset = (roadTileWidth + dividerWidth)/2;

		/*
		Debug.Log ("borderWidth: " + borderWidth);
		Debug.Log ("dividerWidth: " + dividerWidth);
		Debug.Log ("roadTileWidth: " + roadTileWidth);
		Debug.Log ("tileHeight: " + tileHeight);
		*/

		// Generate random numbers

		// set tiles to locations
		nextPosition = originalStartPosition;
		startPosition = originalStartPosition;
		for (int i = 0; i < maxRoadLength; ++i) {
			constructRoad();
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Debug.Log (roadTileQueue.Count);
		Debug.Log (leftBorderQueue.Count);
		Debug.Log (rightBorderQueue.Count);
		Debug.Log (dividerQueue.Count);
		*/

		// After time limit, change road
		timeUntilRoadChange -= Time.deltaTime;
		if (timeUntilRoadChange <= 0) {
			// Generate new time to wait until road change
			timeUntilRoadChange = Random.Range (minChangeTime, maxChangeTime);

			// Generate new numer of lanes

			// Generate starting position of the lanes
			int startLane = (int)Random.Range(0, 4 - numLanes + 1);
			Debug.Log("startLane: " + startLane);
			startPosition.x = originalStartPosition.x + startLane * tileOffset * 2;
			nextPosition.x = startPosition.x;
		}

		// If the car is past a certain point, construct the road up front
		if (roadTileQueue.Peek ().localPosition.z + recycleOffset * tileHeight < Car.getDistanceTravelled ()) {
			constructRoad ();
		}
	}

	private void constructRoad() {
		Transform border = leftBorderQueue.Dequeue ();
		border.localPosition = nextPosition;
		leftBorderQueue.Enqueue (border);
		nextPosition.x += tileOffset;

		Transform roadTile, divider;
		for (int i = 0; i < numLanes - 1; ++i) {
			roadTile = roadTileQueue.Dequeue();
			roadTile.localPosition = nextPosition;
			roadTileQueue.Enqueue(roadTile);
			nextPosition.x += tileOffset;

			divider = dividerQueue.Dequeue();
			divider.localPosition = nextPosition;
			dividerQueue.Enqueue(divider);
			nextPosition.x += tileOffset;
		}

		roadTile = roadTileQueue.Dequeue();
		roadTile.localPosition = nextPosition;
		roadTileQueue.Enqueue(roadTile);
		nextPosition.x += tileOffset;

		border = rightBorderQueue.Dequeue ();
		border.localPosition = nextPosition;
		rightBorderQueue.Enqueue (border);

		nextPosition.x = startPosition.x;
		nextPosition.z += tileHeight;
	}
	
	public static int getNumLanes() {
		return numLanes;
	}

	public static int getMaxLanes() {
		return maxLanes; 
	}

	public static float getRoadWidth() {
		return numLanes * roadTileWidth + (numLanes - 1) * dividerWidth + 2 * borderWidth;
	}
}
