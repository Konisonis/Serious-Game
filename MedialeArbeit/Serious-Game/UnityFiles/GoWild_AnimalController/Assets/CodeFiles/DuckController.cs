using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour {

    public float speed, rotSpeed;
    public Transform grounddetector;
    public bool active;

    private bool isGrounded;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

    void Start()
    {
        active = true;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
            var trans = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            transform.Rotate(0, rot, 0);
            transform.Translate(0, 0, trans);

            if(leftJoyCon.isWalking() && rightJoyCon.isWalking()){
                trans = 1 * Time.deltaTime * speed;
                transform.Translate(0, 0, trans);
            }

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            }

            GetComponent<Rigidbody>().mass = 50 + GetComponent<Rigidbody>().position.y * 0.5f;
        }
    }
}
