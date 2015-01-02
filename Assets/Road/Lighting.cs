using UnityEngine;
using System.Collections.Generic;

public class Lighting : MonoBehaviour {

	public float slider;
	public float hour;
	public float timeOfDay;
	public float speed = 50;

	// Colors
	public Color sunNight;
	public Color sunDay;

	// Lights
	public Light sun;

	// Street Lamps
	public int numLamps = 10;
	public int recycleOffset = 2;
	public Vector3 startPosition;
	public Transform streetLampPrefab;
	public float streetLampDistance;

	private Queue<Transform> streetLampQueue;
	private Vector3 nextPosition;

	// sun stuff
	void OnGUI() {
		if (slider >= 1)
			slider = 0;

		slider = GUI.HorizontalSlider (new Rect (20f, 20f, 200f, 30f), slider, 0f, 1.0f);
		hour = slider * 24;

		sun.transform.localEulerAngles = new Vector3 ((slider * 360) - 90, 90f, 0f);

		slider += Time.deltaTime / speed;

		sun.color = Color.Lerp (sunNight, sunDay, slider * 2);
		sun.intensity = (slider - 0.2f) * 1f;
	}

	// Use this for initialization
	void Start () {
		streetLampQueue = new Queue<Transform> (10);

		for (int i = 0; i < numLamps; ++i) {
			streetLampQueue.Enqueue ((Transform)Instantiate (streetLampPrefab));
		}

		for (int i = 0; i < numLamps; ++i) {
			constructLamps();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Create Street Lamps

		if (streetLampQueue.Peek ().localPosition.z + recycleOffset * streetLampDistance 
		    	< Car.getDistanceTravelled ()) {
			constructLamps ();
		}
	}

	private void constructLamps() {
		Transform lamp = streetLampQueue.Dequeue ();
		lamp.localPosition = nextPosition;
		streetLampQueue.Enqueue (lamp);

		nextPosition.x += RoadManager.getRoadWidth ();

		lamp = streetLampQueue.Dequeue ();
		lamp.localPosition = nextPosition;
		streetLampQueue.Enqueue (lamp);

		nextPosition.x = startPosition.x;
		nextPosition.z += streetLampDistance;
	}
}
