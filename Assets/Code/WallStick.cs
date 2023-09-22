using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{
    public float wallJumpForce = 5f;
    public float wallSlideSpeed = 0.5f; // 墙面滑行速度
    public LayerMask wallLayer; // 设置为"wall" Layer

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private Vector2 wallNormal; // 墙面的法线

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 设置垂直速度为滑行速度

            if (Input.GetButtonDown("Jump"))
            {
                WallJump();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0) // 检查碰撞的物体是否在"wall" Layer
        {
            isTouchingWall = true;
            wallNormal = collision.contacts[0].normal; // 获取碰撞的法线
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
        Vector2 jumpDirection = (-wallNormal + Vector2.up).normalized; // 跳跃方向为墙面法线的反方向加上向上的方向，然后归一化
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);
    }
}