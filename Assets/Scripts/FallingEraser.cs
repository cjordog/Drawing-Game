using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEraser : MonoBehaviour {

	public float fallSpeed = 5.0f;
	public float spinSpeed = 250.0f;
    private float rayLength = .25f;
    private float radius;
    public LayerMask obstacleMask;
    private CircleCollider2D temp;
    private Vector3 tempRay;
    void Start()
    {
        temp = this.gameObject.GetComponent<CircleCollider2D>();
        radius = temp.radius;
        tempRay = Vector3.right;
    }
    void shootRayToDestroy(Vector3 direction)
    {
        for (int i = 0; i < 4; i++)
        {
            RaycastHit2D enemyHit = Physics2D.Raycast(this.transform.position, direction, radius*1.2f, obstacleMask);
            Debug.DrawRay(this.transform.position, direction * radius*1.2f, Color.red);
            if (enemyHit)
            {
                Debug.Log(enemyHit.collider.gameObject.tag);
                if (enemyHit.collider.gameObject.tag == "LineComponent")
                {

                    enemyHit.collider.gameObject.GetComponent<LineScript>().deletThis();

                }

            }
            else
                break;
        }
    }
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + Vector3.down * .175f, .10f);
    }
    */
    void Update() {

		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        
        for (int i = 0; i < 180; i = i + 20)
        {

            tempRay = new Vector3(Mathf.Cos(Mathf.Deg2Rad * i), -Mathf.Sin(Mathf.Deg2Rad * i), 0);
            shootRayToDestroy(tempRay);
        }
    }

	void OnCollisionEnter2D (Collision2D collision)
	{
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if(collision.gameObject.tag == "LineComponent")
            {
                collision.gameObject.GetComponent<LineScript>().deletThis();
            }
            else
            {
                Destroy(this.gameObject);
            }
               
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bounds"))
        {
            Destroy(this.gameObject);
        }

        }
}
