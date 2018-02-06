using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private float speed;
    private float slowdown;
    Controller2D controller;
    // Use this for initialization
    void Start()
    {
        controller = this.GetComponent<Controller2D>();
        rb = this.GetComponent<Rigidbody2D>();
        slowdown = .5f;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
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
    }
}
