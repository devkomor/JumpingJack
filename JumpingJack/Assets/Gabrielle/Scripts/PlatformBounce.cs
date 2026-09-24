using UnityEngine;
using System.Collections.Generic;


public class PlatformBounce : MonoBehaviour
{
    public float jumpForce = 10f;
    void OnCollisionEnter2D(Collision2D collision)
    {
       Rigidbody2D rigidbody = collision.collider.GetComponent<Rigidbody2D>();
       if (rigidbody != null )
        {
            Vector2 velocity = rigidbody.linearVelocity;
            velocity.y = jumpForce;
            rigidbody.linearVelocity= velocity;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
