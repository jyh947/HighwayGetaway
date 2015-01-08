using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {
	
	void OnMouseDown() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
