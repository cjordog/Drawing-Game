using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackAndForth : MonoBehaviour {
	public int maxright;
	public int maxleft; 
	public float speed; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;
	private float max_right;
	private float max_left;


	private Rigidbody2D rb;
	private bool moveRight = true; 
	// Use this for initialization
	void Start () {
		max_right = transform.position.x + maxright;
		max_left = transform.position.x - maxleft; 
		UIHealthScript = HealthDisplay.GetComponent<Health>();
	}

	// Update is called once per frame
	void Update () {

		if (moveRight) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		if(!moveRight) {
			transform.Translate (-Vector3.right * speed * Time.deltaTime);
		}
		if (transform.position.x > max_right) {
			moveRight = false; 
		}
		if (transform.position.x < max_left) {
			moveRight = true; 
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Enemy Damaged Player"); 
			UIHealthScript.loseHealth (); 
			Destroy (gameObject); 
		}
	}


}
