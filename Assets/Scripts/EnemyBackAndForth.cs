using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int maxright;
	public int maxleft; 
	[SerializeField]
	private float speed; 

	private float max_right;
	private float max_left;


	private Rigidbody2D rb;
	private bool moveRight = true; 
	// Use this for initialization
	void Start () {
		max_right = transform.position.x + maxright;
		max_left = transform.position.x - maxleft; 
	}

	// Update is called once per frame
	void Update () {

		if (moveRight) {

			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		if(!moveRight) {
			transform.Translate (-Vector3.right * speed * Time.deltaTime);
		}
		if (transform.position.x > max_right)
			moveRight = false; 
		if (transform.position.x < max_left)
			moveRight = true; 

	}
}
