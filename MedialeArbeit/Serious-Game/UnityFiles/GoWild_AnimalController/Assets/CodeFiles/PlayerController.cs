using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    public Transform grounddetector;

    private bool isGrounded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        var trans = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, 0, rot);
        transform.Translate(0, trans, 0);
    }
}
