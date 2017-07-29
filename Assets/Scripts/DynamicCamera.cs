using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {

    /*
    public Transform player;

    private float damping = 12.0f;
    private float height = 13.0f;
    private float offset = 0.0f;
    private float viewDistance = 5.0f;
    private Vector3 center;
    private Vector3 mousePos;
    private Vector3 cursorPos;
    private Vector3 playerPos;

    void Update()
        {
            mousePos = Input.mousePosition;
            mousePos.z = viewDistance;
            cursorPos  = Camera.main.ScreenToWorldPoint(mousePos);

            playerPos = player.position;

            center = new  Vector3((playerPos.x + cursorPos.x) / 2, playerPos.y, (playerPos.z + cursorPos.z) / 2);

            transform.position = Vector3.Lerp(transform.position, center + new Vector3(0, height, offset), Time.deltaTime * damping);
    }
    */
    public Transform Player;

    private float Damping = 12.0f;
    private float Height = 13.0f;
    private float Offset = 0.0f;
    private float ViewDistance = 5.0f;
    private Vector3 Center;

 
     void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = ViewDistance;
            Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos); 
            Vector3 PlayerPosition = Player.position;

            Center =new  Vector3((PlayerPosition.x + CursorPosition.x) / 2, PlayerPosition.y, (PlayerPosition.z + CursorPosition.z) / 2);

            transform.position = Vector3.Lerp(transform.position, Center +new  Vector3(0, Height, Offset), Time.deltaTime * Damping);
        }
}
