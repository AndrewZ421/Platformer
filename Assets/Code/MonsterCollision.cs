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
            NewPlayerHP hp = collision.gameObject.GetComponent<NewPlayerHP>();
            if (hp != null)
            {
                hp.HP--;
                if (hp.HP < 1)
                {
                    hp.HP = 0;
                }
                else
                {
                    return;
                }
            }

            Gameover();
        }
    }

    void Gameover()
    {
        SoundManager.instance.PlaySoundDie();
        GameoverController.instance.Show();
    }
}
