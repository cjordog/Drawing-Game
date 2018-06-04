using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Transform player;

    
    [SerializeField] float speed = 6f;
    [SerializeField] float xDifference;
    [SerializeField] float yDifference;

    [SerializeField] float yMovementThreshold = 3.5f;
	[SerializeField] float xMovementThreshold = 5f;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.1f;
    void Update()
    {
		xDifference = player.transform.position.x - transform.position.x;
		yDifference = player.transform.position.y - transform.position.y;
		float xTemp = transform.position.x, yTemp = transform.position.y;
		Vector3 moveTemp = transform.position;
		smoothTime = 0.1f/(Mathf.Sqrt(Mathf.Pow(xDifference, 2) + Mathf.Pow(yDifference,2)));
		if (Mathf.Abs(xDifference) >= xMovementThreshold)
		{
			if (xDifference < 0) {
				xTemp = player.transform.position.x + xMovementThreshold;
			} else if (xDifference > 0) {
				xTemp = player.transform.position.x - xMovementThreshold;
			}
			moveTemp = new Vector3 (xTemp, yTemp, transform.position.z);
        }
		if (Mathf.Abs(yDifference) >= yMovementThreshold) {
			if (yDifference < 0) {
				yTemp = player.transform.position.y + yMovementThreshold;
			} else if (yDifference > 0) {
				yTemp = player.transform.position.y - yMovementThreshold;
			}
			moveTemp = new Vector3 (xTemp, yTemp, transform.position.z);
		}
//		transform.position = moveTemp;
		transform.position = Vector3.SmoothDamp(transform.position, moveTemp, ref velocity, smoothTime);
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


