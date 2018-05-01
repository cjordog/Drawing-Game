using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    public Vector3 pos1, pos2;
	public float speed = 1.0f;
    void Start()
    {
        pos1 = this.transform.position;
        pos2 = pos1 - new Vector3(8, 0, 0);
    }
	void Update() {
		transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
	}

}
