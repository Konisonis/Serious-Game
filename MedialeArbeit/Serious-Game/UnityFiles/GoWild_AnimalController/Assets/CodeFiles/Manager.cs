using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public Camera camDog, camDuck;
    public Controller dog, duck;

    private Controller[] animals;
    private Camera[] cams;
    private int position, maxAnimals;

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
    }

    public Controller getAnimal(int num)
    {
        return animals[num];
    }
}
