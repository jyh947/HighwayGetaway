﻿using UnityEngine;
using System.Collections;

public class NonPlayerCar : MonoBehaviour {

	public float position;

	public void stopCar(){
		// 
		gameObject.SetActive( false );
	}

	public void startCar( ){
		gameObject.SetActive( true );
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		position = gameObject.GetComponent<Transform>().localPosition.z;
	}
}
