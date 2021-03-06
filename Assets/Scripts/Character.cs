﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public float rateOfFire = 0.5f;
    public float speed = 5.0f;
    public static float energy = 100;
    public static float maxEnergy = 100;
    public float bulletSpeed = 5000;
    public GameObject gun;
    public GameObject[] bullet;
    public GameObject[] bulletCannon;
    public GameObject[] bulletRapid;
    public GameObject ambientLight;
    public GameObject tutorial;
    public GameObject gameControllerObj;
    public Text energyTextObject;
    public static int healingBar;
    public float bulletCost = 1;
    public static float passiveEnergyLoss = 0.5f;


    private int forceSlow = 1;
    private Vector3 mousePos;
    private Vector3 objectPos;
    private float defaultSpeed = 5.0f;
    private float sprintSpeed = 7.5f;
    private float angle;
    private float energyCounter;
    private GameObject bulletInstance;
    private Text energyText;
    private bool walking = false;
    private bool running = false; //TODO Faster anim when running
    private bool canShoot = true;
    private bool soundPlaying = false;
    private Animator characterAnim;
    private SpriteRenderer ambientRenderer;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private GameObject obj;
    private SpawnEnemy spawnEnemy;
    private bool losingEnergy = false;
    private GameController gameController;


    void Start ()
    {
        Invoke("DestoryTut", 10);
        energy = 100;
        maxEnergy = 100;
        obj = GameObject.FindGameObjectWithTag("Spawn");
        spawnEnemy = obj.GetComponent<SpawnEnemy>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        healingBar = 0;
        energyCounter = energy;
        ambientRenderer = ambientLight.GetComponent<SpriteRenderer>();
        characterAnim = gameObject.GetComponent<Animator>();
        energyText = energyTextObject.GetComponent<Text>();
        gameController = gameControllerObj.GetComponent<GameController>();
	}

    void Update()
    {
        if (spawnEnemy.energyPaused == false && losingEnergy == false)
        {
            losingEnergy = true;
            Invoke("PassiveEnergyLoss", 1.5f);
        }
        else if (spawnEnemy.energyPaused == true)
        {
            losingEnergy = false;
            CancelInvoke("PassiveEnergyLoss");
        }


        if (energy > 100)
        {
            energy = 100;
        }
        if (energy < 0)
        {
            SceneManager.LoadScene("Lose Screen");
        }

        BrightnessUpdater();

        if (Input.GetKeyDown("space"))
        {
            tutorial.SetActive(false);
        }

        //Ui Text
        energyText.text = energy.ToString("f1");

        //Shoot
        if (gameController.rapidFireUpgrade == true)
        {
            if (Input.GetMouseButton(0) && canShoot == true && energy > 1)
            {
                characterAnim.SetBool("Shooting", true);
                Shoot();
                canShoot = false;
                Invoke("BulletDelay", rateOfFire);
                energy -= bulletCost;
            }
        }
        else
        if (Input.GetMouseButtonDown(0) && canShoot == true && energy > 1)
        {
            characterAnim.SetBool("Shooting", true);
            Shoot();
            canShoot = false;
            Invoke("BulletDelay", rateOfFire);
            energy -= bulletCost;
        }

        RotateTowardsMouse();

        Movement2();

        //Test
        if (Input.GetKeyDown("backspace"))
        {
            gameController.rapidFireUpgrade = true;
            rateOfFire = 0.1f;
            bulletCost = 0.5f;
        }
    }

    private void BrightnessUpdater()
    {
        if (energyCounter > energy)
        {
            ambientRenderer.color -= new Color(0, 0, 0, 0.004f) * Mathf.Abs(energy - energyCounter);
            energyCounter = energy;
        }
        else if (energyCounter < energy)
        {
            ambientRenderer.color += new Color(0, 0, 0, 0.004f) * Mathf.Abs(energy - energyCounter);
            energyCounter = energy;
        }
    }

    private void BulletDelay()
    {
        canShoot = true;
        characterAnim.SetBool("Shooting", false);
    }

    private void Shoot()
    {
        if (gameController.rapidFireUpgrade == true)
        {
            if (energy >= 75)
                bulletInstance = Instantiate(bulletRapid[0]);
            else if (energy >= 50 && energy < 75)
                bulletInstance = Instantiate(bulletRapid[1]);
            else if (energy >= 25 && energy < 50)
                bulletInstance = Instantiate(bulletRapid[2]);
            else if (energy < 25)
                bulletInstance = Instantiate(bulletRapid[3]);
            bulletInstance.transform.position = new Vector2(gun.transform.position.x, gun.transform.position.y);
        }
        else if (gameController.cannonUpgrade == true)
        {
            if (energy >= 75)
                bulletInstance = Instantiate(bulletCannon[0]);
            else if (energy >= 50 && energy < 75)
                bulletInstance = Instantiate(bulletCannon[1]);
            else if (energy >= 25 && energy < 50)
                bulletInstance = Instantiate(bulletCannon[2]);
            else if (energy < 25)
                bulletInstance = Instantiate(bulletCannon[3]);
            bulletInstance.transform.position = new Vector2(gun.transform.position.x, gun.transform.position.y);
        }
        else
        {
            if (energy >= 75)
                bulletInstance = Instantiate(bullet[0]);
            else if (energy >= 50 && energy < 75)
                bulletInstance = Instantiate(bullet[1]);
            else if (energy >= 25 && energy < 50)
                bulletInstance = Instantiate(bullet[2]);
            else if (energy < 25)
                bulletInstance = Instantiate(bullet[3]);
            bulletInstance.transform.position = new Vector2(gun.transform.position.x, gun.transform.position.y);
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(new Vector2(0, 1)*(Time.deltaTime * speed));
            if (rb.velocity.y < -Vector2.up.y)
                rb.velocity = new Vector2(rb.velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed / forceSlow);
            walking = true;
            characterAnim.SetBool("Walking", walking);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                soundPlaying = true;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));
            if (rb.velocity.x > -Vector2.left.x)
                rb.velocity = new Vector2(0, rb.velocity.y);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed / forceSlow);
            walking = true;
            characterAnim.SetBool("Walking", walking);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                soundPlaying = true;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(new Vector2(0, -1) * (Time.deltaTime * speed));
            if (rb.velocity.y > Vector2.up.y)
                rb.velocity = new Vector2(rb.velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(-Vector2.up * speed / forceSlow);
            walking = true;
            characterAnim.SetBool("Walking", walking);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                soundPlaying = true;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
            if (rb.velocity.x < Vector2.left.x)
                rb.velocity = new Vector2(0, rb.velocity.y);
            GetComponent<Rigidbody2D>().AddForce(-Vector2.left * speed / forceSlow);
            walking = true;
            characterAnim.SetBool("Walking", walking);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                soundPlaying = true;
            }
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            if (soundPlaying == true)
            {
                soundPlaying = false;
                audioSource.Stop();
            }
            walking = false;
            characterAnim.SetBool("Walking", walking);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            energy -= 0.025f;
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
            //TODO waste more energy
        }
    }

    private void Movement2()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GetComponent<Rigidbody2D>().velocity = targetVelocity * speed;
        walking = true;
        characterAnim.SetBool("Walking", walking);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            soundPlaying = true;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            if (soundPlaying == true)
            {
                soundPlaying = false;
                audioSource.Stop();
            }
            walking = false;
            characterAnim.SetBool("Walking", walking);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            energy -= 0.025f;
            speed = sprintSpeed;
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

    private void PassiveEnergyLoss()
    {
        losingEnergy = true;
        Invoke("PassiveEnergyLoss", 1.5f);
        energy -= passiveEnergyLoss;
    }

    private void DestoryTut()
    {
        tutorial.SetActive(false);
    }
}
