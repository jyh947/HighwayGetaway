using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {
	public GUITexture button;
	public Texture2D onClickButton;
	public Texture2D offClickButton;

	void OnMouseDown() {
		button.GetComponent<GUITexture>().texture = onClickButton;
	}

	void OnMouseUp() {
		if (button.HitTest (Input.mousePosition) != null) {
			button.GetComponent<GUITexture> ().texture = offClickButton;
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
