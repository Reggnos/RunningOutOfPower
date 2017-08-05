using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveDisplay : MonoBehaviour {

    private GameObject obj;
    private SpawnEnemy spawnEnemy;
    private Text text;

    // Use this for initialization
    void Start () {
        obj = GameObject.FindGameObjectWithTag("Spawn");
        spawnEnemy = obj.GetComponent<SpawnEnemy>();
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Wave: " + (spawnEnemy.Wave-1);	
	}
}
