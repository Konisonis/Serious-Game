using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeSim : MonoBehaviour {

    public float duration = 1.0F;
    public Color color0 = Color.red;
    public Color color1 = Color.blue;
    public Light lt;
    private Transform direction;

    void Start()
    {
        lt = GetComponent<Light>();
        direction = lt.transform;
    }
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);
        
    }
}
