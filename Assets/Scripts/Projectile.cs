using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public bool facingRight = true; 
	public int speed = 5; 
	public GameObject player; 
	public GameObject LineComponent; 

	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;

	private int facingRightInt = 0; 
	// Use this for initialization
	void Start () {
		UIHealthScript = HealthDisplay.GetComponent<Health>();

		if (facingRight)
			facingRightInt = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x >= player.transform.position.x + 10 || transform.position.x <= player.transform.position.x - 10)
			Object.Destroy (this.gameObject);
		if(facingRightInt == 1)
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		else
			transform.Translate (Vector3.left * speed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Projectile damaged Player"); 
			UIHealthScript.loseHealth ();  
		}
		Destroy (gameObject); 
	}
}
