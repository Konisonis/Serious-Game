using System.Collections;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour {

    public AudioClip clip;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = clip;

    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource source = GetComponent<AudioSource>();
        if (collision.gameObject.name == "Dog")
        {
            source.volume = 1f;
        } else
        {
            source.volume = (collision.relativeVelocity.magnitude-15) / 8f;
        }
        
        source.Play();
    }
}
