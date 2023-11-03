using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(1); // Each collision with a monster takes away 1 HP
            }
        }
    }
}
