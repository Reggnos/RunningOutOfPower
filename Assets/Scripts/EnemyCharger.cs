using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : MonoBehaviour {


    public float damage = 20.0f;
    public float distance;
    private EnemyBehaviour enemyBehaviour;
    private Vector2 playerLastPosition;
    private bool startChase;
    private float timeToCharge;
    private bool startedCharging = false;
    private bool startedToChargeUp = false;
    private Animator myAnimator;
    public AudioClip hit;
    private AudioSource audioSource;
    private bool runningTowards = false;
    private bool hitSomething = true;

    private void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        audioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            hitSomething = true;
            Debug.Log("hellow");
            if (runningTowards)
            {
                runningTowards = false;
                audioSource.PlayOneShot(hit);

            }
            if (collision.gameObject.tag == "Player")
            {
                Character.energy -= damage;
            }
        }
    }

    void FixedUpdate ()
    {
        startChase = !enemyBehaviour.idle;

       

        if (startChase)
        {
            //Debug.Log(Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position));
            distance = (Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position));

            if (distance < 6.0f  && !startedCharging && enemyBehaviour.isRaycast)
            {
                hitSomething = false;
                startedToChargeUp = true;
                myAnimator.SetBool("startedToChargeUp", startedToChargeUp);
                timeToCharge += Time.deltaTime;
                
                if (distance < 2)
                    enemyBehaviour.speedChase = 1;
                else
                    enemyBehaviour.speedChase = 3;
                playerLastPosition = enemyBehaviour.playerGameObject.transform.position;
                if (timeToCharge > 2)
                {
                    startedCharging = true;
                }
                    
            }
            else if(startedCharging)
            {
                runningTowards = true;
                enemyBehaviour.isChaser = true;
                enemyBehaviour.direction = playerLastPosition - new Vector2(transform.position.x,transform.position.y);
                if(!hitSomething)
                    enemyBehaviour.speedChase = 12f;
                else
                    enemyBehaviour.speedChase = 0f;

                Invoke("Charge",2);
            }
            else
            {
                timeToCharge = 0;
                startedToChargeUp = false;
                myAnimator.SetBool("startedToChargeUp", startedToChargeUp);
                enemyBehaviour.speedChase = 5f;
            }
           
        }


    }

    void Charge()
    {     
        startedToChargeUp = false;
        myAnimator.SetBool("startedToChargeUp", startedToChargeUp);
        myAnimator.SetBool("Hit", true);
        enemyBehaviour.isChaser = false;
        startedCharging = false;
        timeToCharge = 0;
        Invoke("StopToHit", 0.5f);
    }

    void StopToHit()
    {
        myAnimator.SetBool("Hit", false);
    }

    

}
