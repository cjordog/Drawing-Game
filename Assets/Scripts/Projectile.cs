using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public int speed = 5; 
	public Transform player; 
	public GameObject LineComponent; 
	public bool isReflectingProjectile; 
	public bool reflected = false; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;

	private bool facingRight = true; 
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform; 
		HealthDisplay = GameObject.Find ("HealthDisplay"); 
		UIHealthScript = HealthDisplay.GetComponent<Health>();
		if (player.transform.position.x < transform.position.x)
			facingRight = false;
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
		LineScript script = col.gameObject.GetComponent<LineScript> (); 
		if (script != null && !reflected) { //if it hit a line component object
			facingRight = !facingRight; 
			Debug.Log ("reflected");
			reflected = true; 
		}
		if (col.gameObject.tag == "Player") {
			UIHealthScript.loseHealth ();  
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Enemy" && reflected) {
			Debug.Log ("Hit Boss when reflected");
			col.gameObject.GetComponent <BossGuy> ().getInjured (); 
			Destroy (gameObject); 
		} else if (!isReflectingProjectile) {
			Destroy (gameObject); 
		}
		 
	}
}
