using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    public Vector3 pos1, pos2;
    private float speed;
    public bool horizontal;
    void Start()
    {
        pos1 = this.transform.position;
        if(horizontal)
            pos2 = pos1 - new Vector3(10, 0, 0);
       else
            pos2 = pos1 - new Vector3(0, 10, 0);
        speed = Random.Range(.75f, 2f);
    }
	void Update() {
        
        transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
	}

}
