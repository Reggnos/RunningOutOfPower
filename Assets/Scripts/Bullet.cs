﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 5;
    public GameObject character;

    private ParticleSystem spark;
    private Character characterScript;
    private Vector3 mousePos;
    private Vector3 normalizeDirection;

    void Start () {
        spark = GetComponent<ParticleSystem>();
        //spark.Play();
        characterScript = character.GetComponent<Character>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        normalizeDirection = (mousePos - transform.position).normalized;
        Invoke("DestroySelf", 2);
    }
	
	void Update ()
    {

        //transform.position = Vector3.MoveTowards(transform.position, mousePos, bulletSpeed * Time.deltaTime);
        transform.Translate(normalizeDirection * Time.deltaTime * bulletSpeed);
    }

    void DestroySelf()
    {
        if (Character.energy <= 6)
            Character.healingBar--;
        else if (Character.healingBar > 6)
            Character.healingBar -= 2;
        Destroy(gameObject);
    }

    void PlaySystem()
    {
        spark.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && Character.healingBar <12)
        {
            Character.healingBar++;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Enemy" && Character.healingBar <= 6 && Character.healingBar > 0)
        {
            Character.healingBar--;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Enemy" && Character.healingBar > 6)
        {
            Character.healingBar -= 2;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
