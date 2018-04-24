using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEraser : MonoBehaviour {

	public float fallSpeed = 8.0f;
	public float spinSpeed = 250.0f;
    private float rayLength = .5f;
    public LayerMask obstacleMask;
    private CircleCollider2D temp;
    void Start()
    {
        temp = this.gameObject.GetComponent<CircleCollider2D>();
    }
    void shootRayToDestroy(Vector3 direction)
    {
        RaycastHit2D enemyHit = Physics2D.Raycast(this.transform.position + Vector3.down*.35f, direction, rayLength, obstacleMask);
        Debug.DrawRay(this.transform.position - direction * .35f, direction * rayLength, Color.red);
        if (enemyHit)
        {
            Debug.Log(enemyHit.collider.gameObject.tag);
            if (enemyHit.collider.gameObject.tag == "LineComponent")
            {

                enemyHit.collider.gameObject.GetComponent<LineScript>().deletThis();
            }
        }
    }
    void Update() {

		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        shootRayToDestroy(Vector3.up);
        shootRayToDestroy(Vector3.Normalize(-new Vector3(.1f, -.9f, 0)));
        shootRayToDestroy(Vector3.Normalize(-new Vector3(-.1f, -.9f, 0)));
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
		
	}
}
