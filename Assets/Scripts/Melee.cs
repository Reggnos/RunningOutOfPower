using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public GameObject character;
    public AudioClip miss;
    public AudioClip hit; //Todo Add more

    private Animator characterAnim;
    private BoxCollider2D meeleBox;
    private EnemyTest enemytest;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        characterAnim = character.GetComponent<Animator>();
        meeleBox = gameObject.GetComponent<BoxCollider2D>();
        characterAnim.SetBool("Meleeing", false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ActivateBox();
        }
    }

    private void ActivateBox()
    {
        if (meeleBox.enabled == false)
        {
            Invoke("ActivateBox", 0.1f);
            meeleBox.enabled = true;
            characterAnim.SetBool("Meleeing", true);
        }
        else
        {
            characterAnim.SetBool("Meleeing", false);
            meeleBox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(hit);
        }
        else if (collision.gameObject.tag == "")
        {
            audioSource.PlayOneShot(miss);
        }
    }



}





