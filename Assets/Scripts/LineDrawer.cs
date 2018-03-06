using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

    protected Camera m_Camera;
    protected List<Vector3> m_Points;
    public GameObject linecomponent;
    private float compRad;
    private float playerRad;
    private bool stillHeld;
    private float spacing = .10f;
    // Use this for initialization
    void Start () {
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        compRad = linecomponent.GetComponent<CircleCollider2D>().radius;
        playerRad = this.GetComponent<BoxCollider2D>().size.magnitude;
        m_Points = new List<Vector3>();
        stillHeld = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            stillHeld = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            stillHeld = false;
        }
		
	}
    public void addComponent()
    {
        Vector3 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(mousePosition.x, mousePosition.y, 0);
       // Debug.Log(pos);
        if (!m_Points.Contains(pos))
        {


            if (stillHeld && m_Points.Count != 0) //only add dots in between if user is continuously holding the mouse button
            {
                Vector3 lastPoint = m_Points[m_Points.Count - 1];
                float distance = Vector3.Distance(lastPoint, pos);
                if (distance > spacing)
                {
                    int extraNum = Mathf.FloorToInt(distance / spacing);
                    Vector3 temp = pos - lastPoint;
                    for (int i = 1; i < extraNum; i++)
                    {

                        addPoint(lastPoint + (temp / extraNum * i));
                    }
                }
            }

            else if (Vector3.Distance(pos, this.transform.position) < (compRad + playerRad))
            {
                stillHeld = false;
            }
            else if (stillHeld) 
                addPoint(pos);
        }

    }   
    public void deletePoint()
    {
        if(m_Points.Count!=0)
            m_Points.RemoveAt(0);
    }
    private void addPoint(Vector2 pos)
    {
        m_Points.Add(pos);
        Instantiate(linecomponent, pos, Quaternion.identity);
    }
    private void printPoints()
    {
        for(int i=0; i<m_Points.Count;i++)
        {
            Debug.Log(m_Points[i]);
        }
    }
}
