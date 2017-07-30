using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public float rateOfFire = 0.5f;
    public float speed = 5.0f;
    public static float energy = 100;
    public static float maxEnergy = 100;
    public float bulletSpeed = 5000;
    public GameObject gun;
    public GameObject[] bullet;
    public GameObject ambientLight;
    public Text energyTextObject;
    public static int healingBar;
    public float bulletCost = 1;


    private int forceSlow = 1;
    private Vector3 mousePos;
    private Vector3 objectPos;
    private float defaultSpeed = 5.0f;
    private float sprintSpeed = 10.0f;
    private float angle;
    private float energyCounter;
    private GameObject bulletInstance;
    private Text energyText;
    private bool canShoot = true;
    private Animator characterAnim;
    private SpriteRenderer ambientRenderer;


    void Start ()
    {
        healingBar = 0;
        energyCounter = energy;
        ambientRenderer = ambientLight.GetComponent<SpriteRenderer>();
        characterAnim = gameObject.GetComponent<Animator>();
        PassiveEnergyLoss();
        energyText = energyTextObject.GetComponent<Text>();
	}
	
	void Update ()
    {
        if (energy > 100)
        energy = 100;

        BrightnessUpdater();

        //Ui Text
        energyText.text = energy.ToString();

        //Shoot
        if (Input.GetMouseButtonDown(0) && canShoot == true && energy >1)
        {
            characterAnim.SetBool("Shooting", true);
            Shoot();
            canShoot = false;
            Invoke("BulletDelay", rateOfFire);
            energy--;
        }

        RotateTowardsMouse();
        Movement();
    }
    private void BrightnessUpdater()
    {
        if (energyCounter > energy)
        {
            ambientRenderer.color -= new Color(0, 0, 0, 0.008f) * Mathf.Abs(energy - energyCounter);
            energyCounter = energy;
        }
        else if (energyCounter < energy)
        {
            ambientRenderer.color += new Color(0, 0, 0, 0.008f) * Mathf.Abs(energy - energyCounter);
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
        if (energy >=75)
        bulletInstance = Instantiate(bullet[0]);
        else if (energy >= 50 && energy < 75)
        bulletInstance = Instantiate(bullet[1]);
        else if (energy >= 25 && energy < 50)
        bulletInstance = Instantiate(bullet[2]);
        else if (energy < 25)
        bulletInstance = Instantiate(bullet[3]);
        bulletInstance.transform.position = new Vector2(gun.transform.position.x, gun.transform.position.y);
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            energy -= 0.05f;
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
            //TODO waste more energy
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
        Invoke("PassiveEnergyLoss", 1);
        energy -= 0.5f;
    }
}
