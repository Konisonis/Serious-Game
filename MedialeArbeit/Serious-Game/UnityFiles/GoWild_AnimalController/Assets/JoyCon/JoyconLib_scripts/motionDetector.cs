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

	public bool walking;
	public bool inWalkingOrientation;

	// Use this for initialization
	void Start () {
		accelarationVector = Vector3.zero;
		rotationVector = Vector3.zero;

		joyConColor = GetComponent<Renderer>().material.color;

		stopTime = 0;
		walking = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rotationVector.x = joycon.rotation.x;
		rotationVector.y = joycon.rotation.y;
		rotationVector.z = joycon.rotation.z;

		rotationQuaternion = Quaternion.Euler(rotationVector);
		transform.rotation = rotationQuaternion;

		rotationVector.x = Mathf.Round (rotationVector.x);
		rotationVector.y = Mathf.Round (rotationVector.y);
		rotationVector.z = Mathf.Round (rotationVector.z);
				
		accelarationVector.x = Mathf.Round((joycon.accel.x * joycon.accelMagnitude) * 1000);
		accelarationVector.y = Mathf.Round((joycon.accel.y * joycon.accelMagnitude) * 1000);
		accelarationVector.z = Mathf.Round((joycon.accel.z * joycon.accelMagnitude) * 1000);

		if(isWalking()){
			GetComponent<Renderer>().material.color = Color.yellow;
		}else{
			GetComponent<Renderer>().material.color = joyConColor;
		}
	}

	public bool isWalking(){
		if((stopTime + elapsedTimeForHalt) < Time.time){
			walking = false;
		}
		if (isInWalkingOrientation()) {
			if (accelarationVector.z < zBottomMargin) {
				stopTime = Time.time;
				walking = true;
			} else if (accelarationVector.z > zTopMargin) {
				stopTime = Time.time;
				walking = true;
			} else {
			
			}
		}
		return walking;
	}

	public bool isInWalkingOrientation(){
		inWalkingOrientation = 
			(160 < joycon.rotation.y && joycon.rotation.y < 210) &&
			(0 <= joycon.rotation.x && joycon.rotation.x < 50) || (340 < joycon.rotation.x && joycon.rotation.x <= 360) &&
			(240 < joycon.rotation.z && joycon.rotation.z < 310);
		return (inWalkingOrientation);
	}
}
