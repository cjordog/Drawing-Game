using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossGuy : MonoBehaviour {
	private int left = 1; 
	private int right = 2; 
	private int notmoving = 3; 
	private int Bosshealth = 10; 

	public int chargespeed = 10; 
	public int currentChargingDirection; 

	public int angerLevel = 1; 

	public GameObject projectile;
	public GameObject reflectableProjectile; 
	public GameObject destroyingProjectile; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;
	public Transform player;

	public Transform leftBoundary;
	public Transform rightBoundary; 

	private BossHealthBarController healthBar; 

	private bool isCharging = false; 
	private bool isShooting = false; 

	// Use this for initialization
	void Start () {
		ShootThreeProjectiles ();  
		//healthBar = GameObject.Find ("BossHealthBar").GetComponent<BossHealthBarController> (); 
		UIHealthScript = HealthDisplay.GetComponent<Health>();

	}

	// Update is called once per frame
	void Update () {
		PossiblyAttack (); 
		if (isCharging) {
			Charge ();
		}
		if (Bosshealth == 7) {
			angerLevel = 2; 
			chargespeed = 14; 
		}
		if (Bosshealth == 5) {
			angerLevel = 3; 
			chargespeed = 18; 
		}
		if (Bosshealth <= 0) {
			healthBar.gameObject.SetActive (false); 
			Destroy (gameObject); 
		}
		/*
		if (healthBar.currentHealth == 70) {
			angerLevel = 2; 
			chargespeed = 14; 
		}
		if (healthBar.currentHealth == 50) {
			angerLevel = 3; 
			chargespeed = 18; 
		}
		if (healthBar.currentHealth <= 0) {
			healthBar.gameObject.SetActive (false); 
			Destroy (gameObject); 
		}
		*/
		//angry and mega-angry

	}
	public void getInjured(){
		Debug.Log ("Boss injured"); 
		Bosshealth--; 
		//healthBar.takeDamage (5);
		isCharging = false; 
	}
	void PossiblyAttack(){
		if(player.transform.position.x < leftBoundary.transform.position.x && player.transform.position.x > rightBoundary.transform.position.x){
			return; 
		}
		int randomInt = Random.Range (0, 60);
		if (randomInt == 0 && !isCharging && !isShooting) {
			isCharging = true; 
			if (player.transform.position.x <= transform.position.x)
				currentChargingDirection = left;
			else
				currentChargingDirection = right; 

		} else if(randomInt == 1 && !isCharging && !isShooting) {
			ShootThreeProjectiles (); 
		}
	}

	void Charge(){
		if (transform.position.x < leftBoundary.transform.position.x -1 || transform.position.x > rightBoundary.transform.position.x + 1) {
			
			isCharging = false; 
			return; 
		}
		if (currentChargingDirection == left) {
			if (transform.position.x <= player.transform.position.x - 5) {
				isCharging = false; 
				currentChargingDirection = notmoving; 
			} else {
				transform.Translate (Vector3.left * chargespeed * Time.deltaTime);
			}
		}
		else if(currentChargingDirection == right){
			if (transform.position.x >= player.transform.position.x + 5) {
				Debug.Log ("I'm out of range and should be stopping");
				isCharging = false; 
				currentChargingDirection = notmoving; 

			} else {
				transform.Translate (Vector3.right * chargespeed * Time.deltaTime);
			}
		}
	}

	void ShootThreeProjectiles(){
		StartCoroutine (ShootThreeProjectilesCoroutine());

	}
	IEnumerator ShootThreeProjectilesCoroutine()
	{
		isShooting = true; 
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		yield return new WaitForSeconds (1f);

		if (Bosshealth <= 7) {
			int maybeDestroyingProjectile = Random.Range (0, 10);
			if(maybeDestroyingProjectile <= 5)
				Instantiate (destroyingProjectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
			if(maybeDestroyingProjectile > 5)
				Instantiate (reflectableProjectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
			yield return new WaitForSeconds (1f); 
		}
		if (Bosshealth > 7) {
			Instantiate (reflectableProjectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
			yield return new WaitForSeconds (1f); 
		}

		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		isShooting = false; 
	}
	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("I collided with something");
		if (col.gameObject.tag == "Player") {
			UIHealthScript.loseHealth (); 
		}
		LineScript script = col.gameObject.GetComponent<LineScript> ();
		if (script != null) { //if it hit a line component object, destroy
			Debug.Log ("blocked by linecomponent");
			isCharging = false; 
		}
	}
}
