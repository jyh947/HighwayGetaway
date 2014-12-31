using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {
	
	public Transform leftBorderPrefab;
	public Transform rightBorderPrefab;
	public Transform dividerPrefab;
	public Transform roadTilePrefab;
	
	public int minLanes = 2;
	public int maxLanes = 4;
	public int numLanes = 2;
	public int roadLength = 10;
	public int recycleOffset = 5;

	public int maxRoadTileObjects = 60;
	
	public Vector3 startPosition;
	private Vector3 nextPosition;
	
	private Queue<Transform> leftBorderQueue;
	private Queue<Transform> rightBorderQueue;
	private Queue<Transform> dividerQueue;
	private Queue<Transform> roadTileQueue;

	private float tileHeight;
	private float borderWidth;
	private float dividerWidth;
	private float roadTileWidth;
	private float tileOffset;

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

		// set tiles to locations
		nextPosition = startPosition;
		for (int i = 0; i < roadLength; ++i) {
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
	
	public int getNumLanes() {
		return numLanes;
	}
}
