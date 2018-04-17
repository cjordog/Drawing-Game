using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuy : MonoBehaviour {

	public GameObject projectile; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript;
	public GameObject player;

	private bool isCharging = false; 
	// Use this for initialization
	void Start () {
		UIHealthScript = HealthDisplay.GetComponent<Health>();
		ShootThreeProjectiles (); 
	}
	
	// Update is called once per frame
	void Update () {
	}
	void Charge(){
		if (player.transform.position.x < transform.position.x) {
			
		}
		else{
			
		}
	}

	void ShootThreeProjectiles(){
		StartCoroutine (ShootThreeProjectilesCoroutine());


	}
	IEnumerator ShootThreeProjectilesCoroutine()
	{
		Debug.Log ("shooting first projectile");
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y +1), Quaternion.identity);
		yield return new WaitForSeconds (.4f);
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
		yield return new WaitForSeconds (.4f); 
		Instantiate (projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1), Quaternion.identity);

	}
}
