using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public GameObject lamp;

	// Use this for initialization
	void Start ()
    {
        Flicker();
	}


    void Flicker()
    {
        if (lamp.activeInHierarchy == true)
        {
            lamp.SetActive(false);
            Invoke("Flicker", Random.Range(0.1f,0.5f));
        }
        else
        {
            lamp.SetActive(true);
            Invoke("Flicker", Random.Range(0.1f, 0.5f));
        }
    }
}
