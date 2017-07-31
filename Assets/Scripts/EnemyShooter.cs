using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public EnemyBehaviour enemyBehaviour;
    public GameObject enemyBullet;
    public GameObject enemyHead;
    private GameObject bulletInstance;
    public GameObject player;
    public int bulletSpeed ;
    bool startShoot;
    bool wasShot = false;





    void Update ()
    {

        startShoot = !enemyBehaviour.idle;
        if(startShoot && !wasShot)
        {
            Debug.Log(player.transform.position);
            Shoot();         
            if(Vector2.Distance(transform.position,player.transform.position) < 5)
            {
                enemyBehaviour.speedChase = 0;
            }
            else
            {
                enemyBehaviour.speedChase = 5f;
            }
        }
    }

    void Shoot()
    {
        wasShot = true;
        bulletInstance = Instantiate(enemyBullet, enemyHead.transform.position, enemyHead.transform.rotation);

        Invoke("BulletNoMore", 1);

    }

    void BulletNoMore()
    {
        Destroy(bulletInstance,3);
        wasShot = false;
    }
}
