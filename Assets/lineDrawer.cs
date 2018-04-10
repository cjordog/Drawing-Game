//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class LineDrawer : MonoBehaviour {
//
//    protected Camera m_Camera;
//    protected List<Vector2> m_Points;
//    public GameObject linecomponent;
//    // Use this for initialization
//    void Start () {
//        if (m_Camera == null)
//        {
//            m_Camera = Camera.main;
//        }
//        m_Points = new List<Vector2>();
//
//    }
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//    public void addComponent()
//    {
//        Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
//        Vector3 pos = new Vector3(mousePosition.x, mousePosition.y, 0);
//        if (!m_Points.Contains(pos))
//        {
//            m_Points.Add(pos);
//            Instantiate(linecomponent, pos, Quaternion.identity);
//            
//        }
//
//    }
//}
