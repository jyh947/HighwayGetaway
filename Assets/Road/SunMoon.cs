using UnityEngine;
using System.Collections;

public class SunMoon : MonoBehaviour {

	public float slider;
	public float hour;
	public float timeOfDay;
	public float speed = 50;

	// Colors
	public Color sunNight;
	public Color sunDay;

	// Lights
	public Light sun;



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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
