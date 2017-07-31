using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    
    public GameObject playerGameObject;
    private Transform playerTransform;
    public Transform head;
    public GameObject[] waypoints;
    public GameObject[] chasepoints;
    private Quaternion newRotation;
    private Vector3 lastPosition;
    public Vector2 direction ;

    public float rotSpeed;
    public float speedIdle = 3f;
    public float speedChase = 5f;
    private float accuracyWP = 1.0f;
    private float dist;
    private float angle;

    // bool lookAround = false
    public int enemyAngle;
    int searchClosest;
    int currentWP = 0;
    int randomCurrentWP;

    public bool idle = true;
    bool goingClosest = false;
    bool findClosest = true;
    public bool isChaser = false;
    //bool lookAroundRotate = true;




    private void Start()
    {
        currentWP = SearchClosestWaypoint(waypoints,1000);
        playerTransform = playerGameObject.transform;
        randomCurrentWP = (int)Mathf.Round(Random.Range(0f, 3f));
    }

    void FixedUpdate()
    {

        //Debug.Log();
       
        //start of idle walk
        if (idle == true && waypoints.Length > 0)
        {
            enemyAngle = 30;
                   

            if (Vector2.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                randomCurrentWP = (int)Mathf.Round(Random.Range(0f, 3f));
                dist = Vector3.Distance(transform.position, waypoints[randomCurrentWP].transform.position);
                direction = waypoints[randomCurrentWP].transform.position - transform.position;

                if (Physics2D.Raycast(transform.position, direction, dist).collider != null)
                {
                    currentWP = SearchClosestWaypoint(waypoints, currentWP);
                    
                }
                else
                {
                    currentWP = randomCurrentWP;  
                }
                
            }
           
            // Debug.Log(currentWP+" "+ Physics2D.Raycast(transform.position, direction, dist).rigidbody);

            direction = waypoints[currentWP].transform.position - transform.position;
            rotSpeed = 10;
            RotateAndMove(speedIdle);

        }//end of idle walk

        //start of chasing
        if (!isChaser)
        {
            direction = playerTransform.position - transform.position;
            rotSpeed = 20;
        }
        else
        {
            rotSpeed = 0;
        }

        angle = Vector2.Angle(playerTransform.position - transform.position, head.up);

        if (Vector3.Distance(playerTransform.position, transform.position) < 10 && (angle < enemyAngle))
        {
            idle = false;
            
            dist = Vector3.Distance(transform.position, playerGameObject.transform.position);

            KeepChasing(speedChase);
            lastPosition = playerTransform.position;

        }
        else if (idle == false)
        {

            if (Vector2.Distance(lastPosition, transform.position) > 4)
            {
                KeepChasing(speedChase);
            }
            else
            {
                Invoke("Move", 2);
            }

        }
    }

    private void Move()
    {
        idle = true;
    }

    private void KeepChasing(float speed)
    {
        if (Physics2D.Raycast(transform.position, direction, DistanceForRaycast(dist)).collider == null && !goingClosest)
        {
            RotateAndMove(speedChase);            
        }
        else //if(!lookAround)
        {
            goingClosest = true;
            if (findClosest)
            {
                searchClosest = SearchClosestChasepoint(chasepoints);
                findClosest = false;
            }   

            direction = chasepoints[searchClosest].transform.position - transform.position;
            RotateAndMove(speedChase);
            
            
            if (Vector2.Distance(chasepoints[searchClosest].transform.position, transform.position) < accuracyWP)
            {
                idle = true;
                goingClosest = false;
                findClosest = true;
              //  lookAround = true;          
            }

            
        }
       /* else
        {
            LookAround();
        }*/

        
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
        int l = chasepoints.Length, closestWay = 0;
        Vector2 fromEnemyToWaypoint;
        for (int i = 0; i < l; i++)
        {

            dist = Vector3.Distance(transform.position, chasepoints[i].transform.position);
            fromEnemyToWaypoint = chasepoints[i].transform.position - transform.position;

            if (dist < closestDistance && Physics2D.Raycast(transform.position, fromEnemyToWaypoint, DistanceForRaycast(dist)).collider == null)
            {
                closestDistance = dist;
                closestWay = i;
            }
        }

        return closestWay;
    }

    private int SearchClosestWaypoint(GameObject[] waypoints,int skip)
    {
        float closestDistance = Mathf.Infinity;
        int l = waypoints.Length, closestWay = 0;

        for (int i = 0; i < l ; i++)
        {
            if (i != skip)
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

        return closestWay;
    }
 /*   private void LookAround()
    {
       Debug.Log(transform.rotation.eulerAngles.z);
        if (transform.rotation.eulerAngles.z < 30 && lookAroundRotate)
        {
            
            transform.Rotate(new Vector3(0, 0, 40* Time.deltaTime));
        } 
       /* else if (transform.rotation.eulerAngles.z > -30)
        {
            lookAroundRotate = false;
            transform.Rotate(new Vector3(0, 0, -50 * Time.deltaTime));
        }
        else
        {
            lookAroundRotate = true;
            
        }
    }*/
    float DistanceForRaycast(float dist)
    {
        if(dist < 3 )
        return dist;
        else 
        return 3;
    }

}

