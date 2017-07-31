using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    public struct EnemyNumber
    {
        public int chaserCount;
        public int chargerCount;
        public int shooterCount;
        public int sum;

        public EnemyNumber(int p1,int p2, int p3)
            {
            chaserCount = p1;
            chargerCount = p2;
            shooterCount = p3;
            sum = chaserCount + chargerCount + shooterCount;
            }
    };

    static EnemyNumber numberOfEnemies = new EnemyNumber(0,0,0);      

    private EnemyBehaviour enemyBehaviourShooter;
    private EnemyShooter enemyShooter;
    private EnemyBehaviour enemyBehaviourCharger;
    private EnemyShooter enemyCharger;
    private EnemyBehaviour enemyBehaviourChaser;
    private EnemyBehaviour enemyBehaviourChaser2;
    private int randomlyChosenEnemy;

    public float spawnDelay = 5;
    private float spawnDelayCurrently;
    public GameObject player;
    public GameObject[] waypoint;
    public GameObject[] chasepoint;
    public GameObject[] enemyType;
    public int Wave = 1;

    void Start ()
    {
        enemyShooter = enemyType[0].GetComponent<EnemyShooter>();
        enemyBehaviourShooter = enemyType[0].GetComponent<EnemyBehaviour>();
        enemyBehaviourShooter.waypoints = new GameObject[waypoint.Length];
        enemyBehaviourShooter.chasepoints = new GameObject[chasepoint.Length];

        enemyCharger = enemyType[1].GetComponent<EnemyShooter>();
        enemyBehaviourCharger = enemyType[1].GetComponent<EnemyBehaviour>();
        enemyBehaviourCharger.waypoints = new GameObject[waypoint.Length];
        enemyBehaviourCharger.chasepoints = new GameObject[chasepoint.Length];

        enemyBehaviourChaser = enemyType[2].GetComponent<EnemyBehaviour>();
        enemyBehaviourChaser.waypoints = new GameObject[waypoint.Length];
        enemyBehaviourChaser.chasepoints = new GameObject[chasepoint.Length];

        enemyBehaviourChaser2 = enemyType[3].GetComponent<EnemyBehaviour>();
        enemyBehaviourChaser2.waypoints = new GameObject[waypoint.Length];
        enemyBehaviourChaser2.chasepoints = new GameObject[chasepoint.Length];


        enemyBehaviourShooter.playerGameObject = player;
        enemyShooter.player = player;
        enemyBehaviourChaser.playerGameObject = player;
        enemyBehaviourChaser2.playerGameObject = player;
        enemyBehaviourCharger.playerGameObject = player;

        
        InvokeRepeating("MakeEnemy", 3, spawnDelay);
        

        int l = waypoint.Length;
        for (int i = 0; i < l; i++)
        {
            enemyBehaviourShooter.waypoints[i] = waypoint[i];
            enemyBehaviourCharger.waypoints[i] = waypoint[i];
            enemyBehaviourChaser.waypoints[i] = waypoint[i];
            enemyBehaviourChaser2.waypoints[i] = waypoint[i];
        }
        
        l = chasepoint.Length;
        for (int i = 0; i < l; i++)
        {
            enemyBehaviourShooter.chasepoints[i] = chasepoint[i];
            enemyBehaviourCharger.chasepoints[i] = chasepoint[i];
            enemyBehaviourChaser.chasepoints[i] = chasepoint[i];
            enemyBehaviourChaser2.chasepoints[i] = chasepoint[i];
        }
    }

    private void Update()
    {
        
        if(numberOfEnemies.sum == 0)
        switch(Wave)
            {
                case 1:
                    {
                        numberOfEnemies.chaserCount = 20;
                        numberOfEnemies.chargerCount = 3;
                        numberOfEnemies.shooterCount = 2;
                        numberOfEnemies.sum = numberOfEnemies.chaserCount + numberOfEnemies.chargerCount + numberOfEnemies.shooterCount;
                        Wave = 2;
                    }
                    break;
                case 2:
                    {
                        numberOfEnemies.chaserCount = 30;
                        numberOfEnemies.chargerCount = 6;
                        numberOfEnemies.shooterCount = 10;
                        numberOfEnemies.sum = numberOfEnemies.chaserCount + numberOfEnemies.chargerCount + numberOfEnemies.shooterCount;
                        Wave = 3;
                    }
                    break;
                case 3:
                    {
                        numberOfEnemies.chaserCount = 40;
                        numberOfEnemies.chargerCount = 9;
                        numberOfEnemies.shooterCount = 4;
                        numberOfEnemies.sum = numberOfEnemies.chaserCount + numberOfEnemies.chargerCount + numberOfEnemies.shooterCount;
                    }
                    break;
            }
    }

    void MakeEnemy()
    {
        randomlyChosenEnemy = (int)Mathf.Round(Random.Range(0f, 3f));
        if (numberOfEnemies.shooterCount == 0 && randomlyChosenEnemy == 0)
            randomlyChosenEnemy = (int)Mathf.Round(Random.Range(1f, 3f));
        if (numberOfEnemies.chargerCount == 0 && randomlyChosenEnemy == 1)
            while (randomlyChosenEnemy != 1)
                randomlyChosenEnemy = (int)Mathf.Round(Random.Range(0f, 3f));
        if (numberOfEnemies.chaserCount == 0 && randomlyChosenEnemy == 2)
            while (randomlyChosenEnemy != 2)
                randomlyChosenEnemy = (int)Mathf.Round(Random.Range(0f, 3f));
        if (numberOfEnemies.chaserCount == 0 && randomlyChosenEnemy == 3)
            randomlyChosenEnemy = (int)Mathf.Round(Random.Range(0f, 2f));

        switch (randomlyChosenEnemy)
        {
            case 0:
                {
                    if (numberOfEnemies.shooterCount > 0)
                    {
                        GameObject enemyInstance = Instantiate(enemyType[0], transform);
                        numberOfEnemies.shooterCount--;
                    }
                }
                break;
            case 1:
                {
                    if (numberOfEnemies.chargerCount > 0)
                    {
                        GameObject enemyInstance = Instantiate(enemyType[1], transform);
                        numberOfEnemies.chargerCount--;
                    }
                }
                break;
            case 2:
                {
                    if (numberOfEnemies.chaserCount > 0)
                    {
                        GameObject enemyInstance = Instantiate(enemyType[2], transform);
                        numberOfEnemies.chaserCount--;
                    }
                }
                break;
             case 3:
                {
                    if (numberOfEnemies.chaserCount > 0)
                    {
                        GameObject enemyInstance = Instantiate(enemyType[3], transform);
                        numberOfEnemies.chaserCount--;
                    }
                }
                break;
        }
    }
    
}
