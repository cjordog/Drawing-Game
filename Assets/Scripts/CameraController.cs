using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector2 offset;
    private Vector2 playerPos;
    private Vector2 playerScreenPosition;
    private Vector2 screenSize;

    public float dampTime = 1.5f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    // Use this for initialization
    void Start ()
      {
        playerPos = player.transform.position;
        offset = transform.position - player.transform.position;
        screenSize = new Vector2(Screen.width, Screen.height);
        playerScreenPosition = Camera.main.ScreenToWorldPoint(playerPos);
    }

    
    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

}
// LateUpdate is called after Update each frame
    //void LateUpdate ()
    //{
    //transform.position = playerPos + offset;
    //}
    // Update is called once per frame
}


