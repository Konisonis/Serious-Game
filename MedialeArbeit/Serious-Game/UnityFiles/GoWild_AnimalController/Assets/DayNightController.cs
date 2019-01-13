using UnityEngine;
using System.Collections;
 
public class Daynightcontroller : MonoBehaviour {
 
    public Light sun;
    public float secondsInFullDay = 240f;
    [Range(0,1)]
    public float currentTimeOfDay = 0.30f;
    [HideInInspector]
    public float timeMultiplier = 1f;
    
    float sunInitialIntensity;
    
    void Start() {
        sunInitialIntensity = sun.intensity;
    }
    
    void Update() {
        UpdateSun();
 
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
 
        if (currentTimeOfDay >= 1) {
            currentTimeOfDay = 0;
        }
    }
    
    void UpdateSun() {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 90, 0);
 
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.80f) {
            intensityMultiplier = 0;
			timeMultiplier = 2.75f;
        }
        else if (currentTimeOfDay <= 0.27f) {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.27f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.75f) {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.78f) * (1 / 0.02f)));
        }
		else if(currentTimeOfDay > 0.23f && currentTimeOfDay < 0.82f){
			if (timeMultiplier != 1f) {
				timeMultiplier = 1f;
			}
		}
 
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
