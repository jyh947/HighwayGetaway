using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	public GUIText velocityText, distanceText;
	public int counter = 0;

	void Start () {
		instance = this;
		velocityText.enabled = true;
		distanceText.enabled = true;
	}
	
	public static void SetVelocity(){
		instance.velocityText.text = PlayerCar.getSpeed().ToString();
	}
	
	public static void SetDistance(){
		int score = ((int)PlayerCar.getDistanceTraveled ()) / 7;
		instance.distanceText.text = score.ToString();
	}

	void Update () {
		counter++;
		if (counter >= 10) {
			SetVelocity ();
			SetDistance ();
			counter = 0;
		}
	}
}