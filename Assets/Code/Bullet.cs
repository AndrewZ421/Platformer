using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rigidbody2D;

    public float speed = 10f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
            SoundManager.instance.PlaySoundHit();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SoundManager.instance.PlaySoundMiss();
        }
        Destroy(gameObject);
    }
}
