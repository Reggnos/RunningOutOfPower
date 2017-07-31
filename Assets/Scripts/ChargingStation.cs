using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour {

    public bool isStationOn = false;

    private AudioSource audioSource;
    private bool isPlaying = false;
    private Animator chargerAnim;

    void Start()
    {
        chargerAnim = GetComponent<Animator>();
        isStationOn = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isStationOn == true)
        {
            if (isPlaying == false)
            {
                audioSource.Play();
                isPlaying = true;
            }
            chargerAnim.SetBool("Active", true);
            Invoke("TurnOff", 10);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isStationOn == true)
        {
            Character.energy += 0.2f;
            Character.passiveEnergyLoss = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Character.passiveEnergyLoss = 0.5f;
    }

    void TurnOff()
    {
        chargerAnim.SetBool("Active", false);
        audioSource.Stop();
        isStationOn = false;
        isPlaying = false;
    }

}
