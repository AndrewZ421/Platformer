using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{
    public float wallJumpForce = 5f;
    public float wallSlideSpeed = 0.5f; // ǽ�滬���ٶ�
    public LayerMask wallLayer; // ����Ϊ"wall" Layer

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private Vector2 wallNormal; // ǽ��ķ���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // ���ô�ֱ�ٶ�Ϊ�����ٶ�

            if (Input.GetButtonDown("Jump"))
            {
                WallJump();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0) // �����ײ�������Ƿ���"wall" Layer
        {
            isTouchingWall = true;
            wallNormal = collision.contacts[0].normal; // ��ȡ��ײ�ķ���
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
        Vector2 jumpDirection = (-wallNormal + Vector2.up).normalized; // ��Ծ����Ϊǽ�淨�ߵķ�����������ϵķ���Ȼ���һ��
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);
    }
}