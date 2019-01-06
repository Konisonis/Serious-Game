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

    private AudioSource walksound;

	public Transform neckBone;
	private float yCameraRotation, zCameraRotation, yNeckRotation, zNeckRotation;

	void Start () {
        active = true;
        canDig = false;

        walksound = GetComponent<AudioSource>();

		yCameraRotation = 180;
		zCameraRotation = 0;
		yNeckRotation = neckBone.transform.rotation.y;
		zNeckRotation = neckBone.transform.rotation.z;

		Mesh mesh = GetComponentInChildren<MeshFilter> ().mesh;
		Debug.LogWarning(mesh.vertices);

	}

	void Update () {
        if (active)
		{
			move();
			dig();
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

		transform.Rotate(0, 0, rot);
		GetComponent<Rigidbody>().MovePosition(transform.position + facingDirection * Time.deltaTime);

		// Sound of walking
		if (trans > 0.5)
		{
			if (!walksound.isPlaying) walksound.Play(0);
		}
		else walksound.Stop();
		// ----------------
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


		Vector3 cameraRotation = new Vector3 (zCameraRotation, yCameraRotation, 0f);
		GetComponentInChildren<Camera> ().transform.eulerAngles = cameraRotation;

		Vector3 neckRotation = new Vector3 (-yNeckRotation, 0f, -zNeckRotation);
		neckBone.transform.localEulerAngles = neckRotation;
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
