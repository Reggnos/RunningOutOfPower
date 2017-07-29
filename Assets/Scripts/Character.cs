using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int forceSlow = 7;
    public float speed = 5.0f;
    public float energy = 100;
    public float bulletSpeed = 5000;
    public GameObject gun;
    public GameObject bullet;

    private Vector3 mousePos;
    private Vector3 objectPos;
    private float defaultSpeed = 5.0f;
    private float angle;
    private GameObject bulletInstance;



	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        RotateTowardsMouse();
        Movement();
    }

    private void Shoot()
    {
        bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = new Vector2(gun.transform.position.x, gun.transform.position.y);
        //bulletInstance.GetComponent<Rigidbody2D>().AddForce(mousePos * bulletSpeed);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(new Vector2(0, 1)*(Time.deltaTime * speed));
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed / forceSlow);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed / forceSlow);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(new Vector2(0, -1) * (Time.deltaTime * speed));
            GetComponent<Rigidbody2D>().AddForce(-Vector2.up * speed / forceSlow);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
            GetComponent<Rigidbody2D>().AddForce(-Vector2.left * speed / forceSlow);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
        }
    }

    private void RotateTowardsMouse()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
