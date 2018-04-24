using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossGuy : MonoBehaviour {

	public Slider BossHealthSlider;

	public float BossHealth; 

	private int left = 1; 
	private int right = 2; 
	private int notmoving = 3; 

	public int chargespeed = 12; 
	public int currentChargingDirection; 

	public GameObject projectile;
	public GameObject reflectableProjectile; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;
	public Transform player;

	private bool isCharging = false; 
	private bool isShooting = false; 
	// Use this for initialization
	void Start () {
		BossHealth = 1.0f; 
		UIHealthScript = HealthDisplay.GetComponent<Health>();
		ShootThreeProjectiles ();  
	}
	
	// Update is called once per frame
	void Update () {
		BossHealthSlider.value = BossHealth; 
		PossiblyAttack (); 
		if (isCharging) {
			Charge ();
		}
	}
	public void getInjured(){
		BossHealth -= .1f; 
	}
	void PossiblyAttack(){
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
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y +1), Quaternion.identity);
		yield return new WaitForSeconds (.4f);
		Instantiate (reflectableProjectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		yield return new WaitForSeconds (.4f); 
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), Quaternion.identity);
		isShooting = false; 
	}
}
