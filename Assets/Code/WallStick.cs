using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{
    public float wallJumpForce = 5f;
    public float wallSlideSpeed = 0.5f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private Vector2 wallNormal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isTouchingWall = false;
    }

    void Update()
    {
        if (isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                WallJump();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isTouchingWall = true;
            wallNormal = collision.contacts[0].normal;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isTouchingWall = false;
        }
    }

    void WallJump()
    {
        Vector2 jumpDirection = (-wallNormal + Vector2.up).normalized;
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);
    }
}