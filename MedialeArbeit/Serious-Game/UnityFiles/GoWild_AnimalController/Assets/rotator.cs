using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {

	public float speed;
    public float rotSpeed;
	float smooth = 5.0f;

	Vector3 dir = Vector3.zero;

	public JoyconController joycon;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rotationVector = new Vector3(joycon.rotation.x, joycon.rotation.y, joycon.rotation.z);
		Quaternion rotationQuaternion = Quaternion.Euler(rotationVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotationQuaternion,  Time.deltaTime * smooth);
		transform.rotation = rotationQuaternion;
		

		// Vector3 translateVector = new Vector3(joycon.gyro.z * 10, joycon.gyro.y * 10, (joycon.gyro.x) * 10 );
		// transform.Translate(translateVector * Time.deltaTime);
		
		// dir.x = -joycon.accel.y * joycon.accelMagnitude;
        // dir.z = joycon.accel.x * joycon.accelMagnitude;

        // // Make it move 10 meters per second instead of 10 meters per frame...
        // dir *= Time.deltaTime;

        // // Move object
        // transform.Translate(dir * speed);
	}
}
