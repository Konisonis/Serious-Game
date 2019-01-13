using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour {

	public float distance = 500.0f;

	public float scale = 15f;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3(transform.localPosition.x - distance, transform.localPosition.y, transform.localPosition.z);
		transform.localScale = new Vector3(scale,scale,scale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
