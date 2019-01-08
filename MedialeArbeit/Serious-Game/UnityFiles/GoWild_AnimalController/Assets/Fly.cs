using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

	public float horizontalSpeed = 0;
	public float verticalSpeed = 1.5f;
	public float amplitude = 0.5f;
	public int initialPositionY = 0;

	public Vector3 tempPosition;

	// Use this for initialization
	void Start () {
		tempPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tempPosition.x += horizontalSpeed;
		tempPosition.y = initialPositionY + Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
		transform.position = tempPosition;
	}
}
