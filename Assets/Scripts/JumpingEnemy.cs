using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour {

	public Vector3 pos1 = new Vector3(-4,0,0);
	public Vector3 pos2 = new Vector3(4,0,0);
	public float speed = 1.0f;
	public Vector2 jumpForce = new Vector2(0,5);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		int r = Random.Range (1, 100);

		// If the enemy doesn't jump, then have it walk.
		if (r < 10) {
			GetComponent<Rigidbody2D> ().AddForce (jumpForce, ForceMode2D.Impulse);
		}

		else transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

	}
}
