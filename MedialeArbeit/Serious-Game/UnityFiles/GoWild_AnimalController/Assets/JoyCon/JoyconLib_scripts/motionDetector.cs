using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motionDetector : MonoBehaviour {
    public float rotSpeed;
	float smooth = 5.0f;

	public Vector3 accelarationVector;

	public int zTopMargin;
	public int zBottomMargin;
	public int zSmallerThanMargin;
	public int zBiggerThanMargin;

	float stopTime;
	public float elapsedTimeForHalt;

	public JoyconController joycon;
	private Color joyConColor;

	public bool walking;

	// Use this for initialization
	void Start () {
		accelarationVector = Vector3.zero;

		joyConColor = GetComponent<Renderer>().material.color;

		stopTime = 0;
		walking = false;

		//InvokeRepeating("checkIfWalking", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 rotationVector = new Vector3(joycon.rotation.x, joycon.rotation.y, joycon.rotation.z);
		Quaternion rotationQuaternion = Quaternion.Euler(rotationVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotationQuaternion,  Time.deltaTime * smooth);
		transform.rotation = rotationQuaternion;
		
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

		if(accelarationVector.z < zBottomMargin && (160 < joycon.rotation.y && joycon.rotation.y < 210)){
			zSmallerThanMargin++;
			stopTime = Time.time;
			walking = true;
		} else if(accelarationVector.z > zTopMargin){
			zBiggerThanMargin++;
			stopTime = Time.time;
			walking = true;
		} else{
			
		}
		return walking;
	}
}
