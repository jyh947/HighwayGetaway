using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	public void Change (string toChangeTo) {
		Application.LoadLevel (toChangeTo);
	}
}
