using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HP_3 : MonoBehaviour
{
    private SpriteRenderer m_Renderer;
    private float m_Timer = 0;

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Timer = 0;
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > 0.5f)
        {
            m_Timer = 0;
            m_Renderer.enabled = !m_Renderer.enabled;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assuming the PlayerHealth script is attached to the player
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.currentHP = Mathf.Min(hp.currentHP + 1, hp.initHP); // Increase HP but do not exceed maxHP
                hp.playerController.UpdateHPDisplay(hp.currentHP); // Update the HP display
                Destroy(gameObject); // Destroy the item
            }
        }
    }
}
