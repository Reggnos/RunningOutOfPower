using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : MonoBehaviour {


    public float damage = 20.0f;
    public EnemyBehaviour enemyBehaviour;
    private Vector2 playerLastPosition;
    private bool startChase;
    private float timeToCharge;
    private bool startedCharging = false;
    private bool startedToChargeUp = false;
    private Quaternion newRotation;
    private Animator myAnimator;
    public AudioClip hit;
    private AudioSource audioSource;
    private bool runningTowards = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        startChase = !enemyBehaviour.idle;

       

        if (startChase)
        {     
            //Debug.Log(Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position));

            if ((Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position) < 6  || (timeToCharge > 0 && timeToCharge < 2)) && !startedCharging)
            {
                startedToChargeUp = true;
                myAnimator.SetBool("startedToChargeUp", startedToChargeUp);
                timeToCharge += Time.deltaTime;
                enemyBehaviour.speedChase = 0;                
                playerLastPosition = enemyBehaviour.playerGameObject.transform.position;
                if (timeToCharge > 2)
                {
                    startedCharging = true;
                }
                    
            }
            else if(startedCharging)
            {
                enemyBehaviour.isChaser = true;
                enemyBehaviour.direction = playerLastPosition - new Vector2(transform.position.x,transform.position.y);
                enemyBehaviour.speedChase = 11f;
                enemyBehaviour.enemyAngle = 360;

                Invoke("Charge",2);
            }
            else
            {
                startedToChargeUp = false;
                myAnimator.SetBool("startedToChargeUp", startedToChargeUp);
                enemyBehaviour.speedChase = 5f;
            }
           
        }


    }

    void Charge()
    {
        runningTowards = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hellow");
        if (runningTowards)
        {
            runningTowards = false;
            //audioSource.PlayOneShot(hit);
            enemyBehaviour.speedChase = 0.0f;
        }   
        if (collision.gameObject.tag == "Player")
        {
            Character.energy -= damage;
        }
    }

}
