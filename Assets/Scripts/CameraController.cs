using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Transform player;

    private Vector3 moveTemp;
    
    [SerializeField] float speed = 6f;
    [SerializeField] float xDifference;
    [SerializeField] float yDifference;

    [SerializeField] float movementThreshold = 5;

    void Update()
    {
        if (player.transform.position.x > transform.position.x)
            xDifference = player.transform.position.x - transform.position.x;
        else
            xDifference = transform.position.x - player.transform.position.x;
        if (player.transform.position.y > transform.position.y)
            yDifference = player.transform.position.y - transform.position.y;
        else
            yDifference = transform.position.y - player.transform.position.y;

        if (xDifference >= movementThreshold  || yDifference >= movementThreshold)
        {
            moveTemp = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);

        }
    }

    //public GameObject player;
    //private Vector2 offset;
    //private Vector2 playerPos;
    //private Vector2 playerScreenPosition;
    //private Vector2 screenSize;

    //public float dampTime = 0.8f;
    //private Vector3 velocity = Vector3.zero;
    //public Transform target;

    // Use this for initialization



    //void Update()
    //{
    //if (player.transform)
    //{
    //  Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
    //  Vector3 delta = player.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
    // Vector3 destination = transform.position + delta;
    //    transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    //  }

    //}

    /////////////////////////////////////////////////////
    //void Start()
    //{
    //    playerPos = player.transform.position;
    //    offset = transform.position - player.transform.position;
    //    screenSize = new Vector2(Screen.width, Screen.height);
    ///    playerScreenPosition = Camera.main.ScreenToWorldPoint(playerPos);
    // }
    // LateUpdate is called after Update each frame
    //void LateUpdate ()
    //{
    //transform.position = playerPos + offset;
    //}
    // Update is called once per frame
}


