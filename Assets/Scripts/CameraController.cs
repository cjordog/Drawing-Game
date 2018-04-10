using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector2 offset;
    private Vector2 playerPos;
    private Vector2 playerScreenPosition;
    private Vector2 screenSize;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
	public float bufferPercentage = 0.2f;
	private float yBuffer;
	private float xBuffer;

    // Update is called once per frame
    void Update()
    {
		
        if (target)
        {
			
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }

	void Start ()
	{
		offset = transform.position - player.transform.position;
		screenSize = new Vector2(Screen.width, Screen.height);
		xBuffer = screenSize.x * bufferPercentage;
		yBuffer = screenSize.y * bufferPercentage;
	}

	void LateUpdate ()
	{
		playerPos = player.transform.position;
		playerScreenPosition = Camera.main.ScreenToWorldPoint(playerPos);
		if (playerScreenPosition.x < xBuffer || playerScreenPosition.x > screenSize.x - xBuffer || 
			playerScreenPosition.y < yBuffer || playerScreenPosition.y > screenSize.y - yBuffer) {
			Vector2 temp = Camera.main.ScreenToWorldPoint (new Vector2 (Mathf.Clamp (playerScreenPosition.x, xBuffer, screenSize.x - xBuffer), 
				Mathf.Clamp (playerScreenPosition.y, yBuffer, screenSize.y - yBuffer)));
			transform.position = temp + offset;
		}
	}


}
