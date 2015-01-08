using UnityEngine;
using System.Collections;

public class BaseCar : MonoBehaviour {

	// Statics
	public float startX = 0f;
	public float startY = 0.5f;
	public float startZ = 0f;
	public float laneSwitchOffset = 3.375f;

	// CopyCat target
	protected Vector3 target;
	
	public float fract;

	public float speed;

	virtual protected void Start () {
		// Generate random car properties
		speed = Globals.StartingVelocity;
		//int startingLane = (int)Random.Range (0, RoadManager.numLanes);
		int startingLane = 2;
		transform.position = new Vector3 (startX + startingLane * RoadManager.laneSwitchOffset, startY, startZ);

		// Instantiate CopyCat object from your position and rotation
		//target = (Transform)Instantiate (RoadManager.getCopyCat(), transform.localPosition, transform.localRotation);
		target = new Vector3 ();
		target = transform.localPosition;
	}
	
	virtual protected void Update () {

		//transform.Translate(0f, 0f, speed * Time.deltaTime);
		//iTween.MoveTo (gameObject, CopyCat.getTransform().position, Time.deltaTime);
		target.z += speed * Time.deltaTime;
		transform.position = Vector3.Lerp (transform.position, target, fract);
	}

	public float getSpeed()
	{
		return speed;
	}
	
	public void setSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	public virtual Transform getTransform()
	{
		return transform;
	}

	public void moveUp()
	{
		if(speed < Globals.MaxVelocity){
			speed += Globals.SwipeVelocityChange;
		}
		GUIManager.SetUp ();
	}
	
	public void moveDown()
	{
		if(speed > Globals.MinVelocity){
			speed -= Globals.SwipeVelocityChange;
		}
		GUIManager.SetDown ();
	}
	
	public void moveLeft()
	{
		if (target.x > Globals.LeftBorder) {
			target.x += -RoadManager.laneSwitchOffset;
		}
		GUIManager.SetLeft ();
	}
	
	public void moveRight()
	{
		if (target.x < Globals.RightBorder) {
			target.x += RoadManager.laneSwitchOffset;
		}
		GUIManager.SetRight ();
	}
}
