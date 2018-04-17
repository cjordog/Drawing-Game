using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuy : MonoBehaviour {

	public GameObject projectile; 
	// Use this for initialization
	void Start () {
		ShootThreeProjectiles (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void ShootThreeProjectiles(){
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1), Quaternion.identity);
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), Quaternion.identity);

	}
}
