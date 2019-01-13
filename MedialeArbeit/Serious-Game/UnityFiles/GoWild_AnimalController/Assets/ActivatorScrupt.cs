using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorScrupt : MonoBehaviour {

	public GameObject timeController;
	private float currentTimeOfDay;

	// Use this for initialization
	void Start () {
		currentTimeOfDay = timeController.GetComponent<DayNightController>().currentTimeOfDay;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeOfDay = timeController.GetComponent<DayNightController>().currentTimeOfDay;
		if (currentTimeOfDay < 0.27f || currentTimeOfDay >= 0.73f) {
			gameObject.GetComponent<Light>().enabled = true;
		} else {
			gameObject.GetComponent<Light>().enabled = false;
		}
	}
}
