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

    };

    static EnemyNumber numberOfEnemies = new EnemyNumber();     

    private EnemyBehaviour enemyBehaviourShooter;
    private EnemyShooter enemyShooter;
    private EnemyBehaviour enemyBehaviourCharger;
    private EnemyShooter enemyCharger;
    private EnemyBehaviour enemyBehaviourChaser;
    private EnemyBehaviour enemyBehaviourChaser2;
    private int randomlyChosenEnemy;
    private float timer = 0;

    public int enemiesOnScreen = 0;
    public float spawnDelay = 5;
    private float spawnDelayCurrently;
    public GameObject player;
    public GameObject[] waypoint;
    public GameObject[] chasepoint;
    public GameObject[] enemyType;
    public int Wave = 1;
    public bool energyPaused = true;

    void Start ()
    {
        Wave = 1;

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
        Debug.Log(enemiesOnScreen);
        numberOfEnemies.sum = numberOfEnemies.chaserCount + numberOfEnemies.chargerCount + numberOfEnemies.shooterCount;
        if (enemiesOnScreen <= 0)
        {
            energyPaused = true;
            timer += Time.deltaTime;
        }
        if (enemiesOnScreen <= 0 && timer > 15)
        {
            energyPaused = false;
            timer = 0;
            switch (Wave)
            {
                case 1:
                    {
                        Debug.Log("Wave1 Started");
                        numberOfEnemies.chaserCount = 10;
                        numberOfEnemies.chargerCount = 2;
                        numberOfEnemies.shooterCount = 4;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                        Wave = 2;
                    }
                    break;
                case 2:
                    {
                        Debug.Log("Wave2 Started");
                        numberOfEnemies.chaserCount = 20;
                        numberOfEnemies.chargerCount = 4;
                        numberOfEnemies.shooterCount = 9;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                        Wave = 3;
                    }
                    break;
                case 3:
                    {
                        Debug.Log("Wave3 Started");
                        numberOfEnemies.chaserCount = 30;
                        numberOfEnemies.chargerCount = 10;
                        numberOfEnemies.shooterCount = 18;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                    }
                    break;
                case 4:
                    {
                        Debug.Log("Wave3 Started");
                        numberOfEnemies.chaserCount = 40;
                        numberOfEnemies.chargerCount = 10;
                        numberOfEnemies.shooterCount = 25;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                    }
                    break;
                case 5:
                    {
                        Debug.Log("Wave3 Started");
                        numberOfEnemies.chaserCount = 50;
                        numberOfEnemies.chargerCount = 15;
                        numberOfEnemies.shooterCount = 20;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                    }
                    break;
                case 6:
                    {
                        Debug.Log("Wave3 Started");
                        numberOfEnemies.chaserCount = 60;
                        numberOfEnemies.chargerCount = 20;
                        numberOfEnemies.shooterCount = 30;
                        enemiesOnScreen = numberOfEnemies.shooterCount + numberOfEnemies.chargerCount + numberOfEnemies.chaserCount;
                    }
                    break;
            }
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
