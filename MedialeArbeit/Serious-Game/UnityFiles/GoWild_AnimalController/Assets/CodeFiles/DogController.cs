using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : Controller{

    public float speed, rotSpeed, jumpForce;
    public Transform grounddetector;
    private Vector3 groundDetecPos;
    private Rigidbody rbody;

    private bool isGrounded, canDig;
    private GameObject target;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

    private AudioSource walksound;

	public Transform neckBone;
	private float yCameraRotation, zCameraRotation, yNeckRotation, zNeckRotation;

	void Start () {
        active = true;
        canDig = false;
        rbody = GetComponent<Rigidbody>();


        walksound = GetComponent<AudioSource>();

		GetComponentInChildren<Camera> ().transform.Rotate(0f, 0f, 0f);
		yCameraRotation = GetComponentInChildren<Camera> ().transform.eulerAngles.y;
		zCameraRotation = GetComponentInChildren<Camera> ().transform.eulerAngles.x;

		yNeckRotation = neckBone.transform.rotation.y;
		zNeckRotation = neckBone.transform.rotation.z;
	}

	void Update () {
        if (active)
		{
            grounding();
			move();
			dig();
            jump();
			lookAround();
        }
    }

	void move(){
		var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
		var trans = Input.GetAxis("Vertical") * speed;
		Vector3 facingDirection = transform.TransformDirection (new Vector3 (0, trans, 0));

		if (leftJoyCon != null && rightJoyCon != null) {
			if (leftJoyCon.isWalking () && rightJoyCon.isWalking ()) {
				trans = 1 * Time.deltaTime * speed;
				transform.Translate (0, trans, 0);
			}
		}

        //transform.Rotate(0, 0, rot);
        //GetComponent<Rigidbody>().MovePosition(transform.position + facingDirection * Time.deltaTime);
        transform.position = transform.position + Camera.main.transform.forward * trans * Time.deltaTime;

        // Sound of walking
        if (trans > 0.5)
		{
			if (!walksound.isPlaying) walksound.Play(0);
		}
		else walksound.Stop();
		// ----------------
	}

    void jump()
    {
        if (Input.GetButtonDown("Jump") && !canDig && isGrounded)
        {
            rbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

	void dig(){
		if (Input.GetButtonDown("Jump") && canDig)
		{
			Vector3 pos = target.GetComponent<Transform>().position;
			pos.y += 0.5f;
			target.GetComponent<Transform>().position = pos;
		}
	}

	void lookAround(){
		yCameraRotation += 2 * Input.GetAxis ("Mouse X");
		zCameraRotation -= 2 * Input.GetAxis ("Mouse Y");
		yNeckRotation += 2 * Input.GetAxis ("Mouse X");
		zNeckRotation -= 2 * Input.GetAxis ("Mouse Y");


		//Vector3 cameraRotation = new Vector3 (zCameraRotation, yCameraRotation, 0f);
		//GetComponentInChildren<Camera> ().transform.eulerAngles = cameraRotation;

		Vector3 neckRotation = new Vector3 (0f, -yNeckRotation, -zNeckRotation);
		neckBone.transform.localEulerAngles = neckRotation;
	}

    void OnTriggerEnter(Collider other)
    {
        canDig = other.gameObject.CompareTag("Dirt");
        target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        canDig = false;
        target = null;
    }

    void grounding()
    {
        groundDetecPos = grounddetector.transform.position;    
        int layerMask = 1 << 8;
        Collider[] found = Physics.OverlapSphere(groundDetecPos, 0.5f, layerMask);
        isGrounded = found.Length > 0;
    }
}
