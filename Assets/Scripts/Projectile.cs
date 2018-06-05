using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public int speed = 6; 
	public Transform player; 
	public GameObject boss; 
	public bool isReflectingProjectile; 
	public bool isDestroyingProjectile; 
	public bool reflected = false; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;

	private bool facingRight = true; 
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss"); 
		player = GameObject.FindGameObjectWithTag ("Player").transform; 
		HealthDisplay = GameObject.Find ("HealthDisplay"); 
		Physics2D.IgnoreCollision (boss.GetComponent<Collider2D> (), GetComponent<Collider2D> ()); 
		UIHealthScript = HealthDisplay.GetComponent<Health>();
		if (player.transform.position.x < transform.position.x)
			facingRight = false;
		
		speed = speed * boss.GetComponent<BossGuy> ().angerLevel;
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x >= player.transform.position.x + 15 && facingRight) || (transform.position.x <= player.transform.position.x - 15 && !facingRight))
			Object.Destroy (this.gameObject);
		if(facingRight == true)
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		else
			transform.Translate (Vector3.left * speed * Time.deltaTime);
	}


	void OnCollisionEnter2D(Collision2D col){
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "boundary") {
			Destroy (gameObject); 
		}
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Collided with Player");
			UIHealthScript.loseHealth ();  
			Destroy (gameObject);
		} 
		LineScript script = col.gameObject.GetComponent<LineScript> ();
		/*
		if (script != null && isDestroyingProjectile) { //if it hit a line component object, pass through
			Debug.Log("I passed through!");
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
		*/
		if (script != null && isReflectingProjectile) { //if it hit a line component object, get reflected
			facingRight = !facingRight; 
			Debug.Log ("reflected");
			Physics2D.IgnoreCollision (boss.GetComponent<Collider2D> (), GetComponent<Collider2D> (), false);
			reflected = true; 
		}
		if (script != null && !isReflectingProjectile) { //&& !isDestroyingProjectile
			Debug.Log ("I got destroyed"); 
			Destroy (gameObject); 
		}
		if (col.gameObject.tag == "Projectile") {
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}

		if (col.gameObject.tag == "Boss" && reflected) {
			Debug.Log ("Hit Boss when reflected");
			col.gameObject.GetComponent <BossGuy> ().getInjured (); 
			Destroy (gameObject); 
		} 
		 
	}
}
