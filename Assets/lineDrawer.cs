using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineDrawer : MonoBehaviour {

	private float width = 2;
	private float height = 2;
	private float lineWidth = 0.5f;

	private Queue<Vector2> pollingPoints;
	private int maxPoints = 50;

	private Vector2 point1;
	private Vector2 point2;

	private MeshFilter mf;
	private Mesh mesh;

	private Vector3[] vertices;

	private bool isDown = false;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent (typeof(MeshFilter));
		gameObject.AddComponent (typeof(MeshRenderer));
		pollingPoints = new Queue<Vector2>();
		mf = GetComponent<MeshFilter>();
		mesh = new Mesh();
		mf.mesh = mesh;

		point1 = Vector2.zero;

		vertices = new Vector3[4];

//		vertices[0] = new Vector3(0, -lineWidth, 0);
//		vertices[1] = new Vector3(0, lineWidth, 0);

		vertices[0] = new Vector3(0, -0, 0);
		vertices[1] = new Vector3(width / 2, 0, 0);
		vertices[2] = new Vector3(0, height, 0);
		vertices[3] = new Vector3(width, height, 0);

		mesh.vertices = vertices;

		int[] tri = new int[6];

		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;

		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;

		mesh.triangles = tri;

		Vector3[] normals = new Vector3[4];

		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;

		mesh.normals = normals;

		Vector2[] uv = new Vector2[4];

		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);

		mesh.uv = uv;
	}
	
	// Update is called once per frame
	void Update () {
		if (pollingPoints.Count >= maxPoints) {
			pollingPoints.Dequeue();
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			isDown = false;
		}
		if (Input.GetKeyDown(KeyCode.Mouse0) || (isDown && Input.GetAxis("Mouse X") != 0)) {
			isDown = true;
//			pollingPoints.Enqueue (Input.mousePosition);
			Debug.Log("here");
			point2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			Debug.Log (point2);

//			vertices[2] = new Vector3(point2.x, point2.y - lineWidth, 0);
//			vertices[3] = new Vector3(point2.x, point2.y + lineWidth, 0);

			vertices[2] = new Vector3(0, point2.y, 0);
			vertices[3] = new Vector3(point2.x, point2.y, 0);

			mesh.vertices = vertices;

//			int[] tri = new int[6];

//			tri[0] = 3;
//			tri[1] = 2;
//			tri[2] = 1;
//
//			tri[3] = 1;
//			tri[4] = 2;
//			tri[5] = 0;
//
//			tri[0] = 0;
//			tri[1] = 2;
//			tri[2] = 1;
//
//			tri[3] = 2;
//			tri[4] = 3;
//			tri[5] = 1;
//
//			mesh.triangles = tri;
		}
	}
}
