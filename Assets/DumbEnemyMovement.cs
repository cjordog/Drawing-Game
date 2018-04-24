using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemyMovement : MonoBehaviour {
    private float moveSpeed=0.05f;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float moveTime = 5f;
    private float timeCounter = 0f;
    public float distanceToMove = .5f;
    // Use this for initialization
    void Start () {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter = timeCounter + Time.deltaTime;
        if(moveTime/2<timeCounter && timeCounter <= moveTime)
        {
            velocity.x = moveSpeed;
            velocity.y = 0f;

        }
        else if(0< timeCounter && timeCounter <= moveTime/2)
        {
            velocity.x = -moveSpeed;
            velocity.y = 0f;
        }
        else
        {
            timeCounter = 0f;
        }
        transform.Translate(velocity);
	}
}
