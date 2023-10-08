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
            bool bRenderer = m_Renderer.enabled;
            bRenderer = !bRenderer;
            m_Renderer.enabled = bRenderer;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            NewPlayerHP hp = collision.gameObject.GetComponent<NewPlayerHP>();
            if (hp != null)
            {
                hp.HP += 300;
                Destroy(gameObject);
            }
        }
    }
}