using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector2 offset;
    private Vector2 playerPos;
    private Vector2 playerScreenPosition;
    private Vector2 screenSize;

    
	// Use this for initialization
	void Start ()
    {
        playerPos = player.transform.position;
        offset = transform.position - player.transform.position;
        screenSize = new Vector2(Screen.width, Screen.height);
        playerScreenPosition = Camera.main.ScreenToWorldPoint(playerPos);
	}

    // LateUpdate is called after Update each frame
    void LateUpdate ()
    {
        transform.position = playerPos + offset;
	}

}
