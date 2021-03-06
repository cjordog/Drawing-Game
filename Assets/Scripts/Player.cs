﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Controller2D))]
[RequireComponent(typeof(LineDrawer))]
public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    float gravity = -10;
    Vector3 velocity; 
    public float moveSpeed;
    public float jumpHeight = 8f;
    public float timeToJumpApex = 1f;
    float jumpVelocity;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    private float velocityXSmoothing;
    Controller2D controller;
    LineDrawer linedrawer;

    // Use this for initialization
    void Start()
    {
        controller = this.GetComponent<Controller2D>();
        linedrawer = this.GetComponent<LineDrawer>();
        
        //rb = this.GetComponent<Rigidbody2D>();
        //slowdown = .5f;
        moveSpeed = 10f;
        gravity = - (2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float targetVelocityX = input.x * moveSpeed;
		if (velocity.x < 0) {
			Vector3 theScale = transform.localScale;
			theScale.x = -Mathf.Abs (theScale.x);
			transform.localScale = theScale;
		} else if (velocity.x > 0) {
			Vector3 theScale = transform.localScale;
			theScale.x = Mathf.Abs (theScale.x);
			transform.localScale = theScale;
		}
		if (input == Vector2.zero) {
			GetComponent<Animator> ().SetBool ("moving", false);
		} else {
			GetComponent<Animator> ().SetBool ("moving", true);
		}

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(Input.GetMouseButton(0))
        {
            linedrawer.addComponent();
        }
        
    }
   
    /*void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = speed * Vector2.left +rb.velocity.y*Vector2.up;

        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = speed * Vector2.right + rb.velocity.y*Vector2.up;
        }
        else
        {
            rb.velocity = slowdown * rb.velocity.x*new Vector2(1,0)+ rb.velocity.y*new Vector2(0,1);
        }
        //if (Input.GetKeyDown("space")&&)
    }*/
}
