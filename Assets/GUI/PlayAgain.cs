using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {
	public GUITexture button;
	public string toChangeTo;
	public Texture2D originalTexture;
	public Texture2D whenPressedTexture;

	void OnMouseDown() {
		button.GetComponent<GUITexture>().texture = whenPressedTexture;
	}

	void OnMouseUp() {
		button.GetComponent<GUITexture> ().texture = whenPressedTexture;
		if (button.HitTest (Input.mousePosition)) {
			Application.LoadLevel (toChangeTo);
		}
	}
}
