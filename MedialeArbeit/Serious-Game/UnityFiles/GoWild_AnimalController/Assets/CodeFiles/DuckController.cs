﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DuckController : Controller {

    public float speed, rotSpeed, weight;
    public Transform grounddetector;

    private bool isGrounded;

    public motionDetector leftJoyCon;
    public motionDetector rightJoyCon;

    public AudioMixer mixer;
    public AudioSource[] sounds;
    private AudioSource flap;
    private AudioSource wind;
    private AudioSource land;

    void Start()
    {
        active = false;

        sounds = GetComponents<AudioSource>();
        flap = sounds[0];
        wind = sounds[1];
        land = sounds[2];
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

            Rigidbody rbody = GetComponent<Rigidbody>();

            if (Input.GetButtonDown("Jump"))
            {
                rbody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
                playFlapSound();
            }

            float height = rbody.position.y;
            float fallspeed = rbody.velocity.magnitude;
            if (height < 0) height = 0;
            rbody.mass = weight + height * 0.5f;

            // Wind sound
            float windVolume = (fallspeed * 1.2f) + (height * 0.6f) -70f;
            if (windVolume > -10) windVolume = -10;
            mixer.SetFloat("cutoff", 200 + fallspeed * 20f);
            mixer.SetFloat("windvolume", windVolume);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Balloon"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Hard landing
        if (collision.relativeVelocity.magnitude > 20)
        {
            land.Play();
        }
    }

    void playFlapSound()
    {
        flap.pitch = (Random.Range(0.9f, 1.1f));
        flap.PlayOneShot(flap.clip, 0.8f);
    }
}
