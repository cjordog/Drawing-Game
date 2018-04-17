using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour {
	public Image Heart1;
	public Image Heart2;
	public Image Heart3;
	public int lives = 3; 
	public bool isDead; 

	// Use this for initialization
	void Start () {
		isDead = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//pass in lives after loss
	public void loseHealth(){
		lives--; 
		if(lives == 2)
			Heart1.color = new Color32 (0, 0, 0, 255);
		if(lives == 1)
			Heart2.color = new Color32 (0, 0, 0, 255);
		if (lives == 0) {
			Heart3.color = new Color32 (0, 0, 0, 255);
			isDead = true; 
			Debug.Log("Player is Dead :(");
		}
	}
	//resets the hearts to be red
	public void resetHealth(){
		Heart1.color = new Color32 (255, 255, 255, 255);
		Heart2.color = new Color32 (255, 255, 255, 255);
		Heart3.color = new Color32 (255, 255, 255, 255);
	}
	//pass in lives after gain
	public void gainHealth(int healthToGain){
		lives += healthToGain; 
		if(lives == 3)
			Heart1.color = new Color32 (255, 255, 255, 255);
		if(lives == 2)
			Heart2.color = new Color32 (255, 255, 255, 255);
	}
}
