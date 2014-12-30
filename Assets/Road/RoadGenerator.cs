using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour {
	
	public Transform prefab;

	public int numRows;
	public int rowWidth;
	public float recycleOffset;
	
	public Vector3 startPosition;
	private Vector3 nextPosition;
	
	private Queue<Transform> tileQueue;
	
	// Use this for initialization
	void Start () {
		nextPosition = startPosition;
		
		tileQueue = new Queue<Transform> (numRows * rowWidth);
		for (int i = 0; i < numRows * rowWidth; ++i) {
			tileQueue.Enqueue ((Transform)Instantiate (prefab));
		}
		for (int i = 0; i < numRows; ++i) {
			recycle();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (tileQueue.Peek().localPosition.z + recycleOffset < Car.getDistanceTravelled()) {
			recycle();
		}
	}
	
	private void recycle() {
		for (int i = 0; i < rowWidth; ++i) {
			Transform o = tileQueue.Dequeue ();
			o.localPosition = nextPosition;
			nextPosition.x += o.localScale.x;
			tileQueue.Enqueue (o);
		}
		nextPosition.z += tileQueue.Peek ().localScale.z;
		nextPosition.x = startPosition.x;
	}
}
