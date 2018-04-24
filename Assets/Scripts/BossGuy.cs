using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuy : MonoBehaviour {

	public GameObject projectile;
	public int chargespeed = 12; 

	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;
	public GameObject player;

	public bool isCharging = true;
	private bool currentChargingDirection = true; 
	// Use this for initialization
	void Start () {
		UIHealthScript = HealthDisplay.GetComponent<Health>();
		ShootThreeProjectiles ();  
		isCharging = true; 
	}
	
	// Update is called once per frame
	void Update () {

		PossiblyAttack (); 
		if (isCharging) {
			Charge (currentChargingDirection);
		}
	}

	void PossiblyAttack(){
		int randomInt = Random.Range (0, 80);
		if (randomInt == 0 && !isCharging) {
			isCharging = true; 
			if (player.transform.position.x < transform.position.x)
				currentChargingDirection = true;
			else
				currentChargingDirection = false; 

		} else if(randomInt == 1) {
			ShootThreeProjectiles (); 
		}
	}

	void Charge(bool left){
		if (left) {
			if (transform.position.x <= player.transform.position.x - 2) {
				Debug.Log ("I'm out of range and should be stopping");
				isCharging = false; 
			}
			transform.Translate (Vector3.left * chargespeed * Time.deltaTime);
		}
		else {
			if (transform.position.x >= player.transform.position.x + 2) {
				Debug.Log ("I'm out of range and should be stopping");
				isCharging = false; 
			}
			transform.Translate (Vector3.right * chargespeed * Time.deltaTime);
		}
	}

	void ShootThreeProjectiles(){
		StartCoroutine (ShootThreeProjectilesCoroutine());


	}
	IEnumerator ShootThreeProjectilesCoroutine()
	{
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y +1), Quaternion.identity);
		yield return new WaitForSeconds (.4f);
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		yield return new WaitForSeconds (.4f); 
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), Quaternion.identity);

	}
}
