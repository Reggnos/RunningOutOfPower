using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 5;
    public GameObject character;
    public AudioClip shot;
    public AudioClip energyLoss;

    private GameObject gun;
    private ParticleSystem spark;
    private Character characterScript;
    private Vector3 mousePos;
    private Vector3 normalizeDirection;
    private AudioSource audioSource;

    void Start () {
        gun = GameObject.FindGameObjectWithTag("Gun");
        audioSource = gun.GetComponent<AudioSource>();
        audioSource.pitch = (Random.Range(0.6f, 1.5f));
        audioSource.PlayOneShot(shot);
        spark = GetComponent<ParticleSystem>();
        characterScript = character.GetComponent<Character>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        if (gameObject.tag == "BulletRapid")
        normalizeDirection = (mousePos - transform.position +  new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0)).normalized;
        else
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
            if (gameObject.tag == "BulletRapid")
            {
                Character.healingBar += Random.Range(0, 2);
                Destroy(gameObject);
            }
            else
            {
                Character.healingBar++;
                Destroy(gameObject);
            }
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
            audioSource.PlayOneShot(energyLoss);
            Destroy(gameObject);
        }
    }
}
