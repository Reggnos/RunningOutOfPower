using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject character;
    public GameObject doorText;
    public GameObject[] doorSwitches;

    private BoxCollider2D boxCollider;
    private SpriteRenderer sR;
    private bool isLocked = false;
    private AudioSource audioSource;
    private Light2D.LightObstacleGenerator obstacleGen;

    // Use this for initialization
    void Start ()
    {
        obstacleGen = GetComponentInChildren<Light2D.LightObstacleGenerator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
        Debug.Log(obstacleGen);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(character.transform.position, doorSwitches[0].transform.position) <= 1.5 || Vector3.Distance(character.transform.position, doorSwitches[1].transform.position) <= 1.5 && isLocked == false)
            {
                obstacleGen.AdditiveColor = Color.black;
                audioSource.Play();
                isLocked = true;
                sR.enabled = true;
                boxCollider.enabled = true;
                Character.energy -= 10;
            }
        }

        if (Vector3.Distance(character.transform.position, doorSwitches[0].transform.position) <= 1.5 || Vector3.Distance(character.transform.position, doorSwitches[1].transform.position) <= 1.5 && isLocked == false)
        {
            doorText.SetActive(true);
        }
        else
        {
            doorText.SetActive(false);
        }
    }

    void CloseDoor()
    {

    }
}
