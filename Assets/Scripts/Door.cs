using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject character;
    public GameObject doorText;

    private BoxCollider2D boxCollider;
    private SpriteRenderer sR;
    private bool isLocked = false;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(character.transform.position, transform.position) <= 1.5 && isLocked == false)
            {
                audioSource.Play();
                isLocked = true;
                sR.enabled = true;
                boxCollider.enabled = true;
                Character.energy -= 10;
            }
        }

        if (Vector3.Distance(character.transform.position, transform.position) <= 1.5 && isLocked == false)
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
