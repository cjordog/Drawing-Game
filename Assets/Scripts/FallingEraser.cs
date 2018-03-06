using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEraser : MonoBehaviour {

	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;

	void Update() {

		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

	}
}
