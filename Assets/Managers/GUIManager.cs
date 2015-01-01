using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText velocityText, scoreText, policeDistanceText;
	public GUITexture Title, tryAgainText;
	
	private static GUIManager instance;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		Title.enabled = true;
		velocityText.enabled = false;
		scoreText.enabled = false;
		policeDistanceText.enabled = false;
		tryAgainText.enabled = false;
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
		if(Input.GetButtonDown("Fire1")){
			GameEventManager.TriggerGameOver();
		}
	}

	private void GameStart() {
		Title.enabled = false;
		velocityText.enabled = true;
		scoreText.enabled = true;
		policeDistanceText.enabled = true;
		tryAgainText.enabled = false;
		enabled = false;

	}
	
	private void GameOver () {
		Title.enabled = true;
		velocityText.enabled = false;
		scoreText.enabled = false;
		policeDistanceText.enabled = false;
		tryAgainText.enabled = true;
		enabled = true;
	}
}