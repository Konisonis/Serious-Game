using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisioner : MonoBehaviour {

	public AudioSource waterin;
	public AudioSource waterout;

	// Use this for initialization
	void Start () {
		this.waterin = GetComponent<AudioSource>();
		this.waterout = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Dog" || other.gameObject.tag == "Duck") {
			this.waterin.Play();

			Destroy (other.gameObject);
		}
	}

	private void OnCollisionExit(Collision other) {
		if (other.gameObject.tag == "Dog" || other.gameObject.tag == "Duck") {
			this.waterout.Play();

			Destroy (other.gameObject);
		}
	}
}
