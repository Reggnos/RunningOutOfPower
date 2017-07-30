using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour {

    public bool isStationOn = false;

    private SpriteRenderer sR;
    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        isStationOn = false;
        sR = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isStationOn == true)
        {
            if (isPlaying == false)
            {
                //audioSource.Play();
                isPlaying = true;
            }
            sR.color = Color.green;
            Invoke("TurnOff", 10);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isStationOn == true)
        {
            Character.energy += 0.02f;
        }
    }

    void TurnOff()
    {
        isStationOn = false;
        isPlaying = false;
        sR.color = Color.white;
    }

}
