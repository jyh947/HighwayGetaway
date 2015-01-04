using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	public GUIText velocityText, distanceText, directionText;
	public int counter = 0;
	public static bool left = false, right = false, up = false, down = false;

	void Start () {
		instance = this;
		velocityText.enabled = true;
		distanceText.enabled = true;
		directionText.enabled = true;
	}
	
	public static void SetVelocity(){
		instance.velocityText.text = PlayerCar.getSpeed().ToString();
	}
	
	public static void SetDistance(){
		int score = ((int)PlayerCar.getDistanceTraveled ()) / 7;
		instance.distanceText.text = score.ToString();
	}
	
	public static void SetDirection(){
		if (left) {
			instance.directionText.text = "Left";
		} else if (right) {
			instance.directionText.text = "Right";
		} else if (up) {
			instance.directionText.text = "Up";
		} else if (down) {
			instance.directionText.text = "Down";
		} else {
			instance.directionText.text = "N/A";
		}

	}

	void Update () {
		counter++;
		if (counter >= 10) {
			SetVelocity ();
			SetDistance ();
			SetDirection();
			counter = 0;
		}
	}

	public static void SetLeft() {
		left = true;
		right = false;
		up = false;
		down = false;
	}
	public static void SetRight() {
		left = false;
		right = true;
		up = false;
		down = false;
	}
	public static void SetUp() {
		left = false;
		right = false;
		up = true;
		down = false;
	}
	public static void SetDown() {
		left = false;
		right = false;
		up = false;
		down = true;
	}
}