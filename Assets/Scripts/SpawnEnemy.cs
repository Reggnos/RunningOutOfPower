using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private EnemyBehaviour enemyBehaviourShooter;
    private EnemyShooter enemyShooter;
    private EnemyBehaviour enemyBehaviourCharger;
    private EnemyShooter enemyCharger;
    private EnemyBehaviour enemyBehaviourChaser;
   
    public GameObject player;
    public GameObject[] waypoint;
    public GameObject[] chasepoint;
    public GameObject[] enemyType;

    void Start ()
    {        
        enemyShooter = enemyType[0].GetComponent<EnemyShooter>();
        enemyBehaviourShooter = enemyType[0].GetComponent<EnemyBehaviour>();

        enemyCharger = enemyType[1].GetComponent<EnemyShooter>();
        enemyBehaviourCharger = enemyType[1].GetComponent<EnemyBehaviour>();

        enemyBehaviourChaser = enemyType[2].GetComponent<EnemyBehaviour>();

        enemyBehaviourShooter.playerGameObject = player;
        enemyShooter.player = player;
        enemyBehaviourChaser.playerGameObject = player;
        enemyBehaviourCharger.playerGameObject = player;

        InvokeRepeating("MakeEnemy", 3, 5);

        int l = waypoint.Length;
        for (int i = 0; i < l; i++)
        {
            enemyBehaviourShooter.waypoints[i] = waypoint[i];
            enemyBehaviourCharger.waypoints[i] = waypoint[i];
            enemyBehaviourChaser.waypoints[i] = waypoint[i];
        }
        
        l = chasepoint.Length;
        for (int i = 0; i < l; i++)
        {
            enemyBehaviourShooter.chasepoints[i] = chasepoint[i];
            enemyBehaviourCharger.chasepoints[i] = chasepoint[i];
            enemyBehaviourChaser.chasepoints[i] = chasepoint[i];
        }
    }
    
    void MakeEnemy()
    {
       
        GameObject enemyInstance = Instantiate(enemyType[(int)Mathf.Round(Random.Range(0f,2f))], transform);
    }
    
}
