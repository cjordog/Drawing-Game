using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour {
	public Image Heart1;
	public Image Heart2;
	public Image Heart3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void loseHealth(int lives){
		if(lives == 3)
			Heart1.gameObject.SetActive(false);
		if(lives == 2)
			Heart2.gameObject.SetActive(false);
		if(lives == 1)
			Heart3.gameObject.SetActive(false);
	}
}
