using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public GameObject character;

    private Animator characterAnim;
    private BoxCollider2D meeleBox;
    private EnemyTest enemytest;

    // Use this for initialization
    void Start ()
    {
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
            Invoke("ActivateBox", 0.5f);
            meeleBox.enabled = true;
            characterAnim.SetBool("Meleeing", true);
        }
        else
        {
            characterAnim.SetBool("Meleeing", false);
            meeleBox.enabled = false;
        }
    }

    
}





