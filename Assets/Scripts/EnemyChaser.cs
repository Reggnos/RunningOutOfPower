using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour {

    private GameObject character;
    private Animator myAnim;
    private EnemyBehaviour enemyBehaviour;
    
	void Start ()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        character = enemyBehaviour.playerGameObject;
        myAnim = GetComponent<Animator>();
    }
	
	void Update ()
    {
		if (Vector2.Distance(transform.position,character.transform.position) < 3)
        {
            myAnim.SetBool("isAttacking", true);
        }
        else
        {
            myAnim.SetBool("isAttacking", false);
        }

	}
}
