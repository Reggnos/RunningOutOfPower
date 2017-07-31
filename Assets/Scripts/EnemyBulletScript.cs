using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    public float speed = 50;

	
	void Update ()
    {
        transform.Translate(new Vector2(0,1)* speed *Time.deltaTime);
	}
}
