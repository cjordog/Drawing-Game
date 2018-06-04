using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {
    public LayerMask collisionMask; //SHOULD BE OBSTACLE MASK
    public LayerMask enemyMask;
    const float skinWidth = .005f;
    BoxCollider2D collider;

    float maxClimbAngle = 60f;
    float maxDescendAngle = 60f;
    RaycastOrigins raycastorigins;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public CollisionInfo collisions;
    float horizontalRaySpacing;
    float verticalRaySpacing;
    float averageAngle;
    float invincibleTime=1f;
    bool takenDamage;
    Health healthController;
    // Use this for initialization
    void Start() {
        collider = GetComponent<BoxCollider2D>(); //get collider
        healthController = GameObject.Find("HealthDisplay").GetComponent<Health>();
        CalculateRaySpacing(); //only needs to calculate spacing of rays from collider once
    }


    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastorigins.bottomLeft : raycastorigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            RaycastHit2D enemyHit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, enemyMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
            if (enemyHit)
            {
                if (!takenDamage)
                {
                    healthController.loseHealth();
                    takenDamage = true;
                }
            }
            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (i == 0 && (slopeAngle <= maxClimbAngle))
                {
                    if(collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if(slopeAngle!=collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance; //change distance in case it hits something closer.
                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle) //if we can climb it and arent climbing
                {

                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;
                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            //if moving down, then start from the bottom left ray, if not start from top left ray
            Vector2 rayOrigin = (directionY == -1) ? raycastorigins.bottomLeft : raycastorigins.topLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            RaycastHit2D enemyHit = Physics2D.Raycast(rayOrigin, Vector2.right * directionY, rayLength, enemyMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (enemyHit)
            {
                if (!takenDamage)
                {
                    healthController.loseHealth();
                    takenDamage = true;
                }
            }
            if (hit)
            {

                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance; //change distance in case it hits something closer.
                if(collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }
                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
            }
        }
        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastorigins.bottomLeft : raycastorigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle!=collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }
    

    public void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        
        if (averageAngle == 0f)
        {
            averageAngle = slopeAngle;
        }
        else if (Mathf.Abs(slopeAngle - averageAngle) > 10)
        {
            averageAngle = slopeAngle;
        }
        else
        {
            averageAngle = averageAngle + slopeAngle / 2;
        }
        //Debug.Log(velocity.x);
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(averageAngle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(averageAngle * Mathf.Deg2Rad) * moveDistance + Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = averageAngle;
        }
    }

    public void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x); //pick sign
        Vector2 rayOrigin = (directionX == -1) ? raycastorigins.bottomRight : raycastorigins.bottomLeft;
        collisions.climbingSlope = false;
        averageAngle = 0;
        // pick ray based on which way we' re going
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
        if(hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            //hit.normal is normal to the slope
            if(slopeAngle!=0&&slopeAngle<=maxDescendAngle)
            {
                if(Mathf.Sign(hit.normal.x)==directionX)
                {
                    if(hit.distance-skinWidth<=Mathf.Tan(slopeAngle*Mathf.Deg2Rad*Mathf.Abs(velocity.x)))
                        //if close enough for the slope to take effect
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;
                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }

    }

    public void Move(Vector3 velocity)
    {
        
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = velocity;
        if(velocity.y<0)
        {
            DescendSlope(ref velocity);
        }

        if (velocity.x != 0)
            HorizontalCollisions(ref velocity);
        if(velocity.y!=0)
            VerticalCollisions(ref velocity);

        transform.Translate(velocity);
        
    }
    void UpdateRaycastOrigins()
    {
        //change raycasts depending on its movement
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2 );
        raycastorigins.bottomLeft = new Vector2 ( bounds.min.x, bounds.min.y );
        raycastorigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastorigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastorigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
    void CalculateRaySpacing()
    {
        //spacing also based on its collider
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount=Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool climbingSlope, descendingSlope;
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;
        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = descendingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
	struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
	// Update is called once per frame
	void Update()
    {
        if(takenDamage)
        {
            invincibleTime = invincibleTime - Time.deltaTime;
            if (invincibleTime < 0)
            {
                takenDamage = false;
                invincibleTime = 2f;
            }
        }
    }
}
