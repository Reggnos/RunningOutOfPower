using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharger : MonoBehaviour {

   
    public EnemyBehaviour enemyBehaviour;
    private Vector2 playerLastPosition;
    private bool startChase;
    private float timeToCharge;
    private bool startedCharging = false;
    private Quaternion newRotation;

    void FixedUpdate ()
    {
        startChase = !enemyBehaviour.idle;

       

        if (startChase)
        {     
            Debug.Log(Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position));

            if ((Vector2.Distance(transform.position, enemyBehaviour.playerGameObject.transform.position) < 6  || (timeToCharge > 0 && timeToCharge < 2)) && !startedCharging)
            {
                timeToCharge += Time.deltaTime;
                enemyBehaviour.speedChase = 0;                
                playerLastPosition = enemyBehaviour.playerGameObject.transform.position;
                if (timeToCharge > 2)
                    startedCharging = true;
            }
            else if(startedCharging)
            {
                enemyBehaviour.isChaser = true;
                enemyBehaviour.direction = playerLastPosition - new Vector2(transform.position.x,transform.position.y);
                enemyBehaviour.speedChase = 8f;
                enemyBehaviour.enemyAngle = 300;

                Invoke("Charge",2);
            }
            else
            {
                enemyBehaviour.speedChase = 5f;
            }
           
        }


    }

    void Charge()
    {
        enemyBehaviour.isChaser = false;
        startedCharging = false;
        timeToCharge = 0;
        
    }

}
