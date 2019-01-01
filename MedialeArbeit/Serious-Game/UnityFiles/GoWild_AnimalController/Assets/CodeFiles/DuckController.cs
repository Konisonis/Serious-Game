using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : Controller {

    public float speed, rotSpeed, weight;
    public Transform grounddetector;

    private bool isGrounded;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

    void Start()
    {
        active = false;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
            var trans = Input.GetAxis("Vertical") * Time.deltaTime * speed;

			if (leftJoyCon != null && rightJoyCon != null) {
				if (leftJoyCon.isWalking () && rightJoyCon.isWalking ()) {
					trans = 1 * Time.deltaTime * speed;
					transform.Translate (0, 0, trans);
				}
				rot = leftJoyCon.joycon.stick[0] * Time.deltaTime * rotSpeed;
			}

            transform.Rotate(0, rot, 0);
            transform.Translate(0, 0, trans);

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            }

            GetComponent<Rigidbody>().mass = weight + GetComponent<Rigidbody>().position.y * 0.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Balloon"))
        {
            Destroy(other.gameObject);
        }
    }
}
