using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector2 offset;
    private Vector2 playerPos;
    
	// Use this for initialization
	void Start ()
    {
        playerPos = player.transform.position;
        offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called after Update each frame
	void LateUpdate ()
    {
        transform.position = playerPos + offset;
	}

}
