using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Transform player;

    private Vector3 moveTemp;
    
    [SerializeField] float speed = 6f;
    [SerializeField] float xDifference;
    [SerializeField] float yDifference;

    [SerializeField] float yMovementThreshold = 2.5f;
	[SerializeField] float xMovementThreshold = 5f;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.2f;
    void Update()
    {
		xDifference = Mathf.Abs (player.transform.position.x - transform.position.x);
		yDifference = Mathf.Abs (player.transform.position.y - transform.position.y);
        if (xDifference >= xMovementThreshold  || yDifference >= yMovementThreshold)
        {
            moveTemp = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, moveTemp, ref velocity, smoothTime);
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


