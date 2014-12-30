using UnityEngine;
using System.Collections.Generic;

public class GrassManager : MonoBehaviour {

	public Transform prefab;
	public float recycleOffset;

	public Vector3 startPosition;
	private Vector3 nextPosition;

	private Transform frontTransform;
	private Transform backTransform;

	// Use this for initialization
	void Start () {
		nextPosition = startPosition;

		frontTransform = (Transform)Instantiate (prefab);
		backTransform = (Transform)Instantiate (prefab);
		moveTile ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Car.getDistanceTravelled() > frontTransform.localPosition.z - recycleOffset) {
			moveTile ();
		}
	}

	private void moveTile() {
		backTransform.localPosition = nextPosition;
		nextPosition.z += backTransform.localScale.y;

		Transform swap = frontTransform;
		frontTransform = backTransform;
		backTransform = swap;
	}
}
