using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public static float distanceTravelled;

	// Use this for initialization
	void Start () {
		distanceTravelled = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, 5f * Time.deltaTime);
		distanceTravelled = transform.localPosition.z;
	}
}
