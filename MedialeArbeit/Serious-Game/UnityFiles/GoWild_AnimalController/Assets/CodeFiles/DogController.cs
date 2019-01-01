using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : Controller{

    public float speed, rotSpeed;
    public Transform grounddetector;

    private bool isGrounded, canDig;
    private GameObject target;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

	void Start () {
        active = true;
        canDig = false;
	}

	void Update () {
        if (active)
        {
            var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
            var trans = Input.GetAxis("Vertical") * speed;
			Vector3 facingDirection = transform.TransformDirection (new Vector3 (0, trans, 0));

			if (leftJoyCon != null && rightJoyCon != null) {
				if (leftJoyCon.isWalking () && rightJoyCon.isWalking ()) {
					trans = 1 * Time.deltaTime * speed;
					transform.Translate (0, trans, 0);
				}
			}

            transform.Rotate(0, 0, rot);
            //transform.Translate(0, trans, 0);
			GetComponent<Rigidbody>().MovePosition(transform.position + facingDirection * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && canDig)
            {
                Vector3 pos = target.GetComponent<Transform>().position;
                pos.y += 0.5f;
                target.GetComponent<Transform>().position = pos;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        canDig = other.gameObject.CompareTag("Dirt");
        target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canDig)
            target = null;
    }
}
