using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour {

    private float time = 0f;
    private float endTime = 2f;
    private LineDrawer LineDrawerScript;
	// Use this for initialization
	void Awake () {
        LineDrawerScript = GameObject.FindWithTag("Player").GetComponent<LineDrawer>();
        //Debug.Log(LineDrawerScript);
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if (time >= endTime)
        {
            deletThis();
            time = -5000;
        }
	}
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Eraser")
        {
            deletThis();
            LineDrawerScript.deletePoint();
            Destroy(this.gameObject);
        }
    }
    public void deletThis()
    {
        LineDrawerScript.deletePoint();
        Destroy(this.gameObject);
    }
}
