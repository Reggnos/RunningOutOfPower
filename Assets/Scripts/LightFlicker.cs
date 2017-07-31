using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public GameObject lamp;
    public float flickerMin = 0.1f;
    public float flickerMax = 0.5f;
    public bool isFlickering = true;

	void Start ()
    {
        Flicker();
	}


    void Flicker()
    {
        if (isFlickering == true)
        {
            if (lamp.activeInHierarchy == true)
            {
                lamp.SetActive(false);
                Invoke("Flicker", Random.Range(flickerMin, flickerMax));
            }
            else
            {
                lamp.SetActive(true);
                Invoke("Flicker", Random.Range(flickerMin, flickerMax));
            }
        }
        else
        {
            lamp.SetActive(true);
        }
    }
}
