using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    public Transform grounddetector;

    private bool isGrounded;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var rot = (Input.GetAxis("Horizontal") + leftJoyCon.joycon.stick[0]) * Time.deltaTime * rotSpeed;
        var trans = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if(leftJoyCon.isWalking() && rightJoyCon.isWalking()){
            trans = 1 * Time.deltaTime * speed;
            transform.Translate(0, trans, 0);
        }

        transform.Rotate(0, 0, rot);
        transform.Translate(0, trans, 0);
    }
}
