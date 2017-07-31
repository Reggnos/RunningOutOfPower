using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int health = 2;
    private Collider2D myCollider;

    private void Start()
    {
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
        if (other.gameObject.tag == "Meele")
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
            health--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, myCollider);
            //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

}
