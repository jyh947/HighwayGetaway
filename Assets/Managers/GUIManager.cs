using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText velocityText, scoreText, policeDistanceText, tryAgainText;
	public GUITexture Title;
	
	private static GUIManager instance;
	
	void Start () {
		instance = this;
		GameEventManager.Game += Game;
		GameEventManager.GameOver += GameOver;
		Title.enabled = true;
		velocityText.enabled = false;
		scoreText.enabled = false;
		policeDistanceText.enabled = false;
		tryAgainText.enabled = false;
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGame();
		}
		if(Input.GetButtonDown("Fire1")){
			GameEventManager.TriggerGameOver();
		}
	}

	private void Game() {
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