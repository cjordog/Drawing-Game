using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEraser : MonoBehaviour {

	private float InstantiationTimer = 1f;
//	GameObject eraser = GameObject.FindWithTaqg("Eraser");
	public GameObject eraser;

	void Update () {
		CreatePrefab();
	}

	void CreatePrefab()
	{
		InstantiationTimer -= Time.deltaTime;
		if (InstantiationTimer <= 0)
		{
			GameObject eraserCopy = Instantiate(eraser, transform.position, Quaternion.identity) as GameObject;
			eraserCopy.SetActive (true);
			InstantiationTimer = 1f;
		}
	}
}
