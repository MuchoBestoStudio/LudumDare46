using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

    float nRand = 0;
    public float IntensityMin = 5;
	public float IntensvityMax = 10;
	
	void Update ()
    {
        nRand = Random.RandomRange(IntensityMin, IntensvityMax);
        this.transform.GetComponent<Light>().intensity = nRand;
	}
}
