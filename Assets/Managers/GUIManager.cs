using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	
	void Start () {
		instance = this;
		GameEventManager.GameOver += GameOver;
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			GameEventManager.TriggerGameOver();
		}
	}
	
	private void GameOver () {
		enabled = true;
	}
}