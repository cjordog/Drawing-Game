using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUI : MonoBehaviour {


    private Camera cam;
    private LineDrawer LineDrawerScript;
    private float percentage;
    public Texture aTexture;

	// Use this for initialization
	void Start () {

        cam = this.gameObject.GetComponent<Camera>();
        LineDrawerScript = GameObject.FindWithTag("Player").GetComponent<LineDrawer>();
        if (LineDrawerScript == null)
        {
            Debug.Log("error");
        }
        percentage = LineDrawerScript.percentage();
    }
	// Update is called once per frame
	void Update () {
        Debug.Log(percentage);
	}
}
