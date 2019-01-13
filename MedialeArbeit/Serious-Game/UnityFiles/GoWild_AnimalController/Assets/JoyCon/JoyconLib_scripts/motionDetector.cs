using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motionDetector : MonoBehaviour {
    public float rotSpeed;

	public Vector3 accelarationVector;
	public Vector3 rotationVector;
	private Quaternion rotationQuaternion;

	public int zTopMargin;
	public int zBottomMargin;

	float stopTime;
	public float elapsedTimeForHalt;

	public JoyconController joycon;
	private Color joyConColor;

	public bool walking, digging;
	public bool inWalkingOrientation, inDiggingOrientation;
	public bool inGestureMode;

	// Use this for initialization
	void Start () {
		accelarationVector = Vector3.zero;
		rotationVector = Vector3.zero;

		joyConColor = GetComponent<Renderer>().material.color;

		stopTime = 0;
		walking = false;
		digging = false;
	}
	
	void FixedUpdate () {
		if(joycon.joycon != null){
			
		accelarationVector.x = Mathf.Round((joycon.accel.x * joycon.accelMagnitude) * 1000);
		accelarationVector.y = Mathf.Round((joycon.accel.y * joycon.accelMagnitude) * 1000);
		accelarationVector.z = Mathf.Round((joycon.accel.z * joycon.accelMagnitude) * 1000);

		if (joycon.joycon.GetButtonDown (Joycon.Button.SHOULDER_2)) {
			inGestureMode = true;
		} else if (joycon.joycon.GetButtonUp (Joycon.Button.SHOULDER_2)) {
			inGestureMode = false;
			joycon.joycon.Recenter ();
		}


		if (inGestureMode) {

		}
		else{
			rotateModel ();
			setGestureOrientations ();
		}

		//setJoyconColors ();
		}
	}

	private void setGestureOrientations(){
		inWalkingOrientation = isInWalkingOrientation ();
		inDiggingOrientation = isInDiggingOrientation ();
	}

	private void setJoyconColors (){
		if(isWalking()){
			GetComponent<Renderer>().material.color = Color.yellow;
		}else{
			GetComponent<Renderer>().material.color = joyConColor;
		}
	}

	private void rotateModel(){
		rotationVector.x = joycon.rotation.x;
		rotationVector.y = joycon.rotation.y;
		rotationVector.z = joycon.rotation.z;

		rotationQuaternion = Quaternion.Euler(rotationVector);
		transform.rotation = rotationQuaternion;

		rotationVector.x = Mathf.Round (rotationVector.x);
		rotationVector.y = Mathf.Round (rotationVector.y);
		rotationVector.z = Mathf.Round (rotationVector.z);
	}

	public bool isWalking(){
		if((stopTime + elapsedTimeForHalt) < Time.time){
			walking = false;
		}
		if (accelarationVector.z < zBottomMargin) {
			stopTime = Time.time;
			walking = true;
		} else if (accelarationVector.z > zTopMargin) {
			stopTime = Time.time;
			walking = true;
		} else {
		
		}
		return (walking && inWalkingOrientation && inGestureMode);
	}

	// x zwischen 340 und 50; z zwischen 240 und 310; y ist egal
	public bool isInWalkingOrientation(){
		return(
			((0 <= joycon.rotation.x && joycon.rotation.x < 50) || (340 < joycon.rotation.x && joycon.rotation.x <= 360)) &&
			(240 < joycon.rotation.z && joycon.rotation.z < 310)
		);
	}

	public bool isDigging(){
		if((stopTime + elapsedTimeForHalt) < Time.time){
			digging = false;
		}
		if (accelarationVector.z < zBottomMargin) {
			stopTime = Time.time;
			digging = true;
		} else if (accelarationVector.z > zTopMargin) {
			stopTime = Time.time;
			digging = true;
		} else {

		}
		return (digging && inDiggingOrientation && inGestureMode);
	}

	//x ist egal, y zwischen 130 und 240, z zwischen 311 und 70
	//z zwischen 311 und 70
	public bool isInDiggingOrientation(){
		return(
			(130 < joycon.rotation.y && joycon.rotation.y < 240) &&
			((311 <= joycon.rotation.z && joycon.rotation.z < 360) || (0 <= joycon.rotation.z && joycon.rotation.z < 70) || (110 <= joycon.rotation.z && joycon.rotation.z < 230))
		);
	}
}
