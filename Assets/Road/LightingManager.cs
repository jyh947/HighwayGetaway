﻿using UnityEngine;
using System.Collections.Generic;

public class LightingManager : MonoBehaviour {

	public static float slider;
	public float intensity;
	public float hour;
	public float timeOfDay;
	public float speed = 50;

	public float dawnThreshold;
	public float duskThreshold;

	// Colors
	public Color sunNight;
	public Color sunDay;

	// Lights
	public Light sun;

	// Street Lamps
	public int numLamps = 6;
	public int recycleOffset = 2;
	public Vector3 startPosition;
	public Transform streetLampPrefab;
	public float streetLampDistance;
	public float roadWidth = 10f;
	public static float streetLampIntensity = 0.6f;

	private static Queue<Transform> streetLampQueue;
	private Vector3 nextPosition;
	private int roadSide = 1;

	// sun stuff
	void OnGUI() {
		if (slider >= 1)
			slider = 0;

		slider = GUI.HorizontalSlider (new Rect (20f, 20f, 200f, 30f), slider, 0f, 1.0f);
		hour = slider * 24;

		sun.transform.localEulerAngles = new Vector3 ((slider * 360) - 90, 90f, 0f);

		slider += Time.deltaTime / speed;

		sun.color = Color.Lerp (sunNight, sunDay, slider * 2);
		sun.intensity = intensity;//(slider - 0.2f) * 1f;
	}

	// Use this for initialization
	void Start () {
		streetLampQueue = new Queue<Transform> (numLamps);

		Vector3 rotatedAngle = new Vector3 (0f, 180f, 0f);
		for (int i = 0; i < numLamps / 2; ++i) {
			streetLampQueue.Enqueue ((Transform)Instantiate (streetLampPrefab));
			streetLampQueue.Enqueue ((Transform)Instantiate (streetLampPrefab, startPosition,
			                                                 Quaternion.Euler(rotatedAngle)));
		}

		nextPosition = startPosition;
		for (int i = 0; i < numLamps; ++i) {
			constructLamps();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Create Street Lamps

		if (streetLampQueue.Peek ().localPosition.z + recycleOffset * streetLampDistance 
		    	< PlayerCar.getDistanceTraveled ()) {
			constructLamps ();
		}

		if ((0 < slider && slider < dawnThreshold) || (duskThreshold < slider && slider < 1)) {
			turnLampsOn();
		} else {
			turnLampsOff();
		}
	}

	private void constructLamps() {
		Transform lamp = streetLampQueue.Dequeue ();
		lamp.localPosition = nextPosition;
		streetLampQueue.Enqueue (lamp);

		nextPosition.x += roadSide * roadWidth;
		nextPosition.z += streetLampDistance;

		roadSide *= -1;
	}

	public static void turnLampsOff() {
		foreach (Transform lamp in streetLampQueue) {
			lamp.Find("Spotlight").GetComponent<Light>().intensity = 0;
		}
	}

	public static void turnLampsOn() {
		foreach (Transform lamp in streetLampQueue) {
			lamp.Find("Spotlight").GetComponent<Light>().intensity = streetLampIntensity;
		}
	}
}
