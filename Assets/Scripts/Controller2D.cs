using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    const float skinWidth = .015f;
    BoxCollider2D collider;

    RaycastOrigins raycastorigins;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    // Use this for initialization
    void Start () {
        collider = GetComponent<BoxCollider2D>();
	}

    void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for(int i =0; i <verticalRayCount;i++)
        {
            Debug.DrawRay(raycastorigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        Debug.Log(collider.bounds);
        bounds.Expand(skinWidth );
        raycastorigins.bottomLeft = new Vector2 ( bounds.min.x, bounds.min.y );
        raycastorigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastorigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastorigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth );

        horizontalRayCount=Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRaySpacing - 1);
    }
	struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
	// Update is called once per frame
	
}
