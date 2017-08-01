using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour {

    public float damage = 0.05f;

    private GameObject character;
    private Animator myAnim;
    private EnemyBehaviour enemyBehaviour;
    private AudioSource audioSource;
    private bool isPlaying = false;
    private GameObject mainCamera;
    
	void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = GetComponent<AudioSource>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        character = enemyBehaviour.playerGameObject;
        myAnim = GetComponent<Animator>();
    }
	
	void Update ()
    {
		if (Vector2.Distance(transform.position,character.transform.position) < 3)
        {
            if (isPlaying == false)
            {
                audioSource.Play();
                isPlaying = true;
            }
            //Shake();
            myAnim.SetBool("isAttacking", true);
            Character.energy -= damage;
        }
        else
        {
            myAnim.SetBool("isAttacking", false);
            audioSource.Stop();
            isPlaying = false;
        }

	}

    void Shake()
    {
        mainCamera.transform.position = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
    }
}
