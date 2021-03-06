﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    
    public GameObject playerGameObject;
    public GameObject[] waypoints;
    public GameObject[] chasepoints;
    private EnemyTest enemyTest;
    private Transform playerTransform;
    public Transform head;    
    private Quaternion newRotation;
    private Vector3 lastPosition;
    public Vector2 direction ;

    public float rotSpeed;
    public float speedIdle = 3f;
    public float speedChase = 5f;
    private float accuracyWP = 0.2f;  
    private float dist;
    private float angle;
    // bool lookAround = false
    public int enemyAngle;
    private int currentWP = 0;
    private int randomCurrentWP;
    private int[] currentWParray;
    private int searchNext;
    private int searchClosestToEnemy;
    private float goToRandomNumber = 0;
    float waitedTooLong = 0;


    public bool idle = true;
    bool findClosest = true;
    public bool isChaser = false;
    bool next = false;
    bool wasChasing = false;
    public bool isRaycast = false;
    bool nowCheckpoints = false;
    //bool lookAroundRotate = true;




    private void Start()
    {
        
        enemyTest = GetComponent<EnemyTest>();
        currentWParray = new int[4];
        currentWP = SearchClosestWaypoint(waypoints);
        playerTransform = playerGameObject.transform;
        randomCurrentWP = (int)Mathf.Round(Random.Range(0f, waypoints.Length - 1));
        direction = waypoints[currentWP].transform.position - transform.position;
    }

    void FixedUpdate()
    {
       
        //start of idle walk
        if (idle == true && waypoints.Length > 0)
        {
            
            enemyAngle = 40;
            waitedTooLong += Time.deltaTime;
            
            if (Vector2.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP || wasChasing || waitedTooLong > 8)
            {
                waitedTooLong = 0;
               
                wasChasing = false;

                currentWParray[0] = SearchClosestWaypoint(waypoints, currentWP);
                currentWParray[1] = SearchClosestWaypoint(waypoints, currentWP, currentWParray[0]);
                if (currentWParray[1] != 0)
                {
                    goToRandomNumber++;
                    currentWParray[2] = SearchClosestWaypoint(waypoints, currentWP, currentWParray[0], currentWParray[1]);
                    if (currentWParray[2] != 0)
                    {
                        goToRandomNumber++;
                        currentWParray[3] = SearchClosestWaypoint(waypoints, currentWP, currentWParray[0], currentWParray[1], currentWParray[2]);
                        if (currentWParray[3] != 0)
                            goToRandomNumber++;
                    }
                }


                randomCurrentWP = (int)Mathf.Round(Random.Range(0f, goToRandomNumber));
                currentWP = currentWParray[randomCurrentWP];
                dist = Vector3.Distance(transform.position, waypoints[currentWP].transform.position);
                direction = waypoints[currentWP].transform.position - transform.position;

                goToRandomNumber = 0;

            }
           
            // Debug.Log(currentWP+" "+ Physics2D.Raycast(transform.position, direction, dist).rigidbody);

            direction = waypoints[currentWP].transform.position - transform.position;
            rotSpeed = 15;
            RotateAndMove(speedIdle);

        }//end of idle walk

        //start of chasing
        if (!isChaser)
        {
            direction = playerGameObject.transform.position - transform.position;
            rotSpeed = 25;
            
        }
        else
        {
            enemyAngle = 180;
            rotSpeed = 0;
        }

        dist = Vector3.Distance(transform.position, playerGameObject.transform.position);
        angle = Vector2.Angle(playerTransform.position - transform.position, head.up);

        if (dist < 10 && Physics2D.Raycast(transform.position, direction, dist).collider == null)
            isRaycast = true;
        else
            isRaycast = false;


        if (dist < 12 && ((angle < enemyAngle) || enemyTest.hit) && isRaycast)
        {
            
            idle = false;
            
            KeepChasing(false);
            lastPosition = playerTransform.position;
        }
        else if (idle == false) //behind wall or character ran too far
        {
            
            if (Vector2.Distance(lastPosition, transform.position) > 2 && !nowCheckpoints)
                {
                    direction = lastPosition - transform.position;
                    KeepChasing(false);
                
                }
                else if(isRaycast)
                {
                    Invoke("Move", 2);
                   
                 }
                else
                {
                
                nowCheckpoints = true;
                  KeepChasing(true);
                } 
                
        }
        
    }

    private void Move()
    {
        enemyTest.hit = false;
        idle = true;
    }

    private void KeepChasing(bool chasingPlayerBehindWall)
    {
        
        if (!chasingPlayerBehindWall)
        {           
            RotateAndMove(speedChase);            
        }
        else 
        {            
            
            if (findClosest)
            {
                searchClosestToEnemy = SearchClosestChasepoint(chasepoints);
                if (searchClosestToEnemy == 1000)
                {
                    idle = true;
                    return;
                    
                }

                Debug.Log(searchClosestToEnemy);
                findClosest = false;
                
                if(searchClosestToEnemy % 2 == 0)
                {
                    searchNext = searchClosestToEnemy + 1;
                    Debug.Log(searchNext);
                }
                else
                {
                    searchNext = searchClosestToEnemy - 1;
                    Debug.Log(searchNext);
                }
            }   

            if(!next)
            {
                direction = chasepoints[searchClosestToEnemy].transform.position - transform.position;
                
            }
            else
            {
                direction = chasepoints[searchNext].transform.position - transform.position;              
            }

            RotateAndMove(speedIdle);

            if (Vector2.Distance(chasepoints[searchClosestToEnemy].transform.position, transform.position) < accuracyWP && !next)
            {              
                next = true;               
            }
            else if(Vector2.Distance(chasepoints[searchNext].transform.position, transform.position) < accuracyWP && next)
            {
                next = false;
                findClosest = true;
                idle = true;
                wasChasing = true;
                nowCheckpoints = false;
            }
            

        }
          
    }

    void RotateAndMove(float speed)
    {
        newRotation = Quaternion.LookRotation(direction, new Vector3(0, 0, -1) * Time.deltaTime * rotSpeed);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        transform.Translate(0, Time.deltaTime * speed, 0);
    }

    private int SearchClosestChasepoint(GameObject[] chasepoints)
    {
       
        float closestDistance = Mathf.Infinity;
        int l = chasepoints.Length, closestWay = 1000;
        Vector2 fromEnemyToWaypoint;

        for (int i = 0; i < l; i++)
        {           
              dist = Vector2.Distance(transform.position, chasepoints[i].transform.position);
              fromEnemyToWaypoint = chasepoints[i].transform.position - transform.position;
                

              if (dist < closestDistance && Physics2D.Raycast(transform.position, fromEnemyToWaypoint, dist).collider == null)
              {
                    closestDistance = dist;
                    closestWay = i;
              }            
        }
            return closestWay;
       
    }

    private int SearchClosestWaypoint(GameObject[] waypoints)
    {
        float closestDistance = Mathf.Infinity;
        int l = waypoints.Length, closestWay = 0;

        for (int i = 0; i < l; i++)
        {         
                dist = Vector3.Distance(transform.position, waypoints[i].transform.position);
                Vector2 fromEnemyToWaypoint = waypoints[i].transform.position - transform.position;

                if (dist < closestDistance && Physics2D.Raycast(transform.position, fromEnemyToWaypoint, dist).collider == null)
                {
                    closestDistance = dist;
                    closestWay = i;
                }          
        }

        return closestWay;
    }

    private int SearchClosestWaypoint(GameObject[] waypoints,int skipOriginal,int skip1 = 1000,int skip2 = 1000, int skip3 = 1000)
    {
        float closestDistance = Mathf.Infinity;
        int l = waypoints.Length, closestWay = 0;
        

        for (int i = 1; i < l ; i++)
        {
            if (i != skipOriginal && i != skip1 && i != skip2 && i != skip3)
            {
                dist = Vector3.Distance(transform.position, waypoints[i].transform.position);
                Vector2 fromEnemyToWaypoint = waypoints[i].transform.position - transform.position;

                if (dist < closestDistance && Physics2D.Raycast(transform.position, fromEnemyToWaypoint, dist).collider == null)
                {
                    closestDistance = dist;
                    closestWay = i;
                }

            }
        
        }
        //Debug.Log(closestWay);
        return closestWay;
    }
 
   

}

