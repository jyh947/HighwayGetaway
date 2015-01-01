using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	public GUIText velocityText, distanceText;

	void Start () {
		instance = this;
	}
	
	public static void SetVelocity(){
		instance.velocityText.text = Car.getSpeed().ToString();
	}
	
	public static void SetDistance(){
		instance.distanceText.text = Car.getDistanceTravelled().ToString();
	}
}