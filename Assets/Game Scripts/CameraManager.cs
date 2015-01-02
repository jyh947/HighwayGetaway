using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float cameraX;
	public float cameraY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(cameraX, cameraY, CopyCat.getDistanceTraveled() - 7f);
	}
}
