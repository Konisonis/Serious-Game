using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public Camera camDog, camDuck;
    public Controller dog, duck;

    private Controller[] animals;
    private Camera[] cams;
    private int position, maxAnimals;

    private AudioSource audioSource;
    public AudioClip soundDog;
    public AudioClip soundDuck;

	void Start () {
        maxAnimals = 2;

        position = 0;
        animals = new Controller[maxAnimals];
        cams = new Camera[maxAnimals];

        animals[0] = dog;
        cams[0] = camDog;
        animals[1] = duck;
        camDuck.enabled = false;
        cams[1] = camDuck;

        audioSource = GetComponent<AudioSource>();
	}


    void Update() {
        if (Input.GetButtonDown("Switch"))
        {
            switchState();
            position++;
            if (position >= maxAnimals)
            {
                position = 0;
            }
            switchState();
        }
    }

    private void switchState()
    {
        animals[position].active = !animals[position].active;
        cams[position].enabled = !cams[position].enabled;
        playAnimalSound();
        changeListener();
    }

    public Controller getAnimal(int num)
    {
        return animals[num];
    }

    private void playAnimalSound()
    {
        switch (position)
        {
            case 0:
                audioSource.clip = soundDog;
                break;
            case 1:
                audioSource.clip = soundDuck;
                break;
        }
        audioSource.Play(0);
    }

    private void changeListener()
    {
        switch (position)
        {
            case 0:
                GameObject.Find("DuckCamera").GetComponent<AudioListener>().enabled = false;
                GameObject.Find("DogCamera").GetComponent<AudioListener>().enabled = true;
                break;
            case 1:
                GameObject.Find("DuckCamera").GetComponent<AudioListener>().enabled = true;
                GameObject.Find("DogCamera").GetComponent<AudioListener>().enabled = false;
                break;
        }
    }
}