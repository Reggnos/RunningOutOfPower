﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour {

    public GameObject character;
    public ParticleSystem particles;
    public GameObject frier;
    public GameObject lightElectric;

    private Collider2D frierCol;
    private AudioSource audioSource;
    private bool isOn = false;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        frierCol = frier.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(character.transform.position, transform.position) <= 1.5 && frierCol.enabled == false)
            {
                //TODO ADD TEXT
                Character.energy -= 10;
                lightElectric.SetActive(true);
                frierCol.enabled = true;
                particles.Play();
                Invoke("TurnOff", 5);
                audioSource.Play();
            }
        }
    }

    void TurnOff()
    {
        audioSource.Stop();
        isOn = false;
        lightElectric.SetActive(false);
        frierCol.enabled = false;
        particles.Stop();
    }
}
