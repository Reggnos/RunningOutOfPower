using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecterScript : MonoBehaviour {

    public GameObject projecter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (projecter.activeInHierarchy)
        {
            projecter.SetActive(false);
        }
        else
        {
            projecter.SetActive(true);
        }

    }
}
