using UnityEngine;
using System.Collections;

public static class GameEventManager {
	
	public delegate void GameEvent();
	
	public static event GameEvent TitleScreen, Game, GameOver;


	public static void TriggerGame(){
		if(Game != null){
			Game();
		}
	}
	
	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
}