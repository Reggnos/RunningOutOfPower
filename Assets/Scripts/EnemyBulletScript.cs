using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    public float speed = 50;
    public float damage = 2.0f  ;

    private Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    void Update ()
    {
        transform.Translate(new Vector2(0,1)* speed *Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, myCollider);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Character.energy -= damage;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
