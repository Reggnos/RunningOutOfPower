using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 5;

    private Vector3 mousePos;
    private Vector3 normalizeDirection;

    void Start () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        normalizeDirection = (mousePos - transform.position).normalized;
        Invoke("DestroySelf", 5);
    }
	
	void Update ()
    {

        //transform.position = Vector3.MoveTowards(transform.position, mousePos, bulletSpeed * Time.deltaTime);
        transform.Translate(normalizeDirection * Time.deltaTime * bulletSpeed);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
