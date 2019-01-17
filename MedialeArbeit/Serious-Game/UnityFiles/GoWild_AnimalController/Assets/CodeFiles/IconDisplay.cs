using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconDisplay : MonoBehaviour {

    public DogController dog;
    public DuckController duck;

    public Texture dogWalk, dogDig, duckStand, duckFly;

    private RawImage icon;

	void Start () {
        icon = GetComponent<RawImage>();
	}
	
	void FixedUpdate () {
        if (dog.active)
        {
            if (dog.canDig)
            {
                icon.texture = dogDig;
            }
            else
                icon.texture = dogWalk;
        }
        else
        {
            if (duck.isGrounded)
            {
                icon.texture = duckStand;
            }
            else
                icon.texture = duckFly;
        }
	}
}
