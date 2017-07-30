﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject character;
    public GameObject pauseScreen;
    public GameObject[] chargingStations;

    private Character characterScript;
    private GameObject selectedStation;
    private ChargingStation chargingScript;

	// Use this for initialization
	void Start ()
    {
        characterScript = character.GetComponent<Character>();
        Invoke("ActivateStation",5);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndPlay();
        }
    }

    public void PauseAndPlay()
    {
        if (pauseScreen.activeInHierarchy == false)
            {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            characterScript.enabled = false;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            characterScript.enabled = true;
        }
    }

    public void ActivateStation()
    {
        selectedStation = chargingStations[Random.Range(0, chargingStations.Length)];
        Debug.Log("Activating Station" + selectedStation.name);
        chargingScript = selectedStation.GetComponent<ChargingStation>();
        chargingScript.isStationOn = true;
        Invoke("ActivateStation", 15);
    }
}