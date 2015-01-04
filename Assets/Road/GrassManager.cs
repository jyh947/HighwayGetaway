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
		Vector3 rotation = new Vector3 (90f, 0f, 0f);
		frontTransform = (Transform)Instantiate (prefab, startPosition, Quaternion.Euler(rotation));
		backTransform = (Transform)Instantiate (prefab, startPosition, Quaternion.Euler(rotation));
		moveTile ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerCar.getDistanceTraveled() > frontTransform.localPosition.z - recycleOffset) {
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
