using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {
	
	public Transform leftBorderPrefab;
	public Transform rightBorderPrefab;
	public Transform dividerPrefab;
	public Transform roadTilePrefab;

	public float pubTileWidth = 1.75f;

	public int minLanes = 2;
	public int maxLanes = 4;
	public int numLanes = 2;
	public int roadLength = 60;
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

	// Use this for initialization
	void Start () {
		// Construct queues
		leftBorderQueue = new Queue<Transform> (maxRoadTileObjects);
		rightBorderQueue = new Queue<Transform> (maxRoadTileObjects);
		dividerQueue = new Queue<Transform> (maxRoadTileObjects);
		roadTileQueue = new Queue<Transform> (maxRoadTileObjects);

		// Generate road assets
		for (int i = 0; i < maxRoadTileObjects; ++i) {
			Debug.LogWarning("made obj");
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
		/*
		Debug.Log ("borderWidth: " + borderWidth);
		Debug.Log ("dividerWidth: " + dividerWidth);
		Debug.Log ("roadTileWidth: " + roadTileWidth);
		Debug.Log ("tileHeight: " + tileHeight);
		*/

		// set tiles to locations
		for (int i = 0; i < maxRoadTileObjects; ++i) {
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
		if (leftBorderQueue.Peek ().localPosition.z + recycleOffset * tileHeight < Car.getDistanceTravelled ()) {
			constructRoad ();
		}
	}

	private void constructRoad() {
		Transform border = leftBorderQueue.Dequeue ();
		border.localPosition = nextPosition;
		leftBorderQueue.Enqueue (border);
		nextPosition.x += pubTileWidth;

		Transform roadTile, divider;
		Debug.Log ("numlanes: " + numLanes);
		for (int i = 0; i < numLanes - 1; ++i) {
			Debug.Log("made lane");
			roadTile = roadTileQueue.Dequeue();
			roadTile.localPosition = nextPosition;
			roadTileQueue.Enqueue(roadTile);
			nextPosition.x += pubTileWidth;

			divider = dividerQueue.Dequeue();
			divider.localPosition = nextPosition;
			dividerQueue.Enqueue(divider);
			nextPosition.x += pubTileWidth;
		}
		Debug.Log ("over");

		roadTile = roadTileQueue.Dequeue();
		roadTile.localPosition = nextPosition;
		roadTileQueue.Enqueue(roadTile);
		nextPosition.x += pubTileWidth;

		border = rightBorderQueue.Dequeue ();
		border.localPosition = nextPosition;
		rightBorderQueue.Enqueue (border);

		nextPosition.x = startPosition.x;
		nextPosition.z += tileHeight;
	}
}
