using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int numLives; 
	public bool alive; 
	[SerializeField]
	private Health playerHealth; 

	//private Canvas UI;
	private GameObject UI;  
	private Health loseHealthScript;

	// Use this for initialization
	void Start () {
		alive = true; 
		playerHealth = GameObject.Find ("Canvas").GetComponent<Health>();
		UI = GameObject.FindGameObjectWithTag ("UI");
		loseHealthScript = UI.GetComponentInChildren<Health> ();
	}

	// Update is called once per frame
	void Update () {
		if (numLives == 0)
			alive = false; 
	}
	public void injured(){
		loseHealthScript.loseHealth (numLives);
		numLives--; 
	}
}
