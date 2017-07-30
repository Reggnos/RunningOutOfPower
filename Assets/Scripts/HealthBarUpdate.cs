using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdate : MonoBehaviour {

    public Sprite[] healingBarSprites;

    private Image imageComp;

	// Use this for initialization
	void Start ()
    {
        imageComp = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		imageComp.sprite = healingBarSprites[Character.healingBar];
    }
}
