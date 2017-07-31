using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public string enemyType;
    public int health;
    private Collider2D myCollider;
    public bool hit = false;

    private void Start()
    {
        if (enemyType == "Charger")
        {
            health = 10;
        }
        else if (enemyType == "Chaser")
        {
            health = 2;
        }
        else if (enemyType == "Chaser2")
        {
            health = 2;
        }
        else if (enemyType == "Shooter")
        {
            health = 5;
        }
        myCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(myCollider, myCollider);
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Meele" && enemyType == "Shooter" || enemyType == "Chaser2")
        {
            {
                if (Character.healingBar == 12)
                {
                    Character.energy += (Character.maxEnergy * 81) / 100;
                }
                else if (Character.healingBar == 11)
                {
                    Character.energy += (Character.maxEnergy * 69) / 100;
                }
                else if (Character.healingBar == 10)
                {
                    Character.energy += (Character.maxEnergy * 57) / 100;
                }
                else if (Character.healingBar == 9)
                {
                    Character.energy += (Character.maxEnergy * 47) / 100;
                }
                else if (Character.healingBar == 8)
                {
                    Character.energy += (Character.maxEnergy * 38) / 100;
                }
                else if (Character.healingBar == 7)
                {
                    Character.energy += (Character.maxEnergy * 31) / 100;
                }
                else if (Character.healingBar == 6)
                {
                    Character.energy += (Character.maxEnergy * 25) / 100;
                }
                else if (Character.healingBar == 5)
                {
                    Character.energy += (Character.maxEnergy * 20) / 100;
                }
                else if (Character.healingBar == 4)
                {
                    Character.energy += (Character.maxEnergy * 16) / 100;
                }
                else if (Character.healingBar == 3)
                {
                    Character.energy += (Character.maxEnergy * 12) / 100;
                }
                else if (Character.healingBar == 2)
                {
                    Character.energy += (Character.maxEnergy * 7) / 100;
                }
                else if (Character.healingBar == 1)
                {
                    Character.energy += (Character.maxEnergy * 3) / 100;
                }
            };
            Character.healingBar = 0;
            health -= 2;
            hit = true;
        }
        else if (other.gameObject.tag == "Frier")
        {
            health -= 3;
        }
        else if (other.gameObject.tag == "Meele")
        {
            health -= 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, myCollider);
            //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            hit = true;
        }
        
    }



}
