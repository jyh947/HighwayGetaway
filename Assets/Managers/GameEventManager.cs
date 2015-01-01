using UnityEngine;
using System.Collections;

public static class GameEventManager {
	
	public delegate void GameEvent();
	
	public static event GameEvent GameOver;
	
	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
}