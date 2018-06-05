using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossHealthBarController : MonoBehaviour {

	private Slider healthBar; 
	public int currentHealth = 100; 
	bool isDead = false; 
	// Use this for initialization
	void Start () {
		healthBar = GetComponent<Slider> (); 
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = currentHealth; 
	}

	public void takeDamage(int damage)
	{
		if (healthBar.value <= 0) {
			isDead = true; 
			Debug.Log ("Boss is Dead"); 
		}
		else
			currentHealth -= damage; 
	}
}
