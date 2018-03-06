using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int numLives; 
	public bool alive; 
	[SerializeField]
	private GameObject HealthDisplay;
	private Health UIHealthScript; 	 
	// Use this for initialization
	void Start () {
		alive = true; 
		UIHealthScript = HealthDisplay.GetComponent<Health>();
	}

	// Update is called once per frame
	void Update () {
		if (UIHealthScript.isDead == true)
			alive = false; 
	}

}
