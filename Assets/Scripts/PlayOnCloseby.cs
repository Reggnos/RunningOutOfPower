using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayOnCloseby : MonoBehaviour {

    public GameObject character;
    public float distance = 5.0f;
    public float minRepeatTime = 0.5f;
    public float maxRepeatTime = 1.5f;
    public AudioClip snd;

    private AudioSource audioSource;
    private bool isPlaying = false;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		if (Vector3.Distance(transform.position,character.transform.position) < distance && isPlaying == false)
        {
            isPlaying = true;
            PlaySound();
        }
        else if (Vector3.Distance(transform.position, character.transform.position) > distance)
        {
            isPlaying = false;
            CancelInvoke("PlaySound");
        }


	}

    void PlaySound()
    {
        audioSource.PlayOneShot(snd);
        Invoke("PlaySound", Random.Range(minRepeatTime, maxRepeatTime = 1.5f));
    }
}
