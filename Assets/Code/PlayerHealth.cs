using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int initHP = 3; // Init health points
    public int currentHP; // Current health points
    public PlayerController playerController;

    void Start()
    {
        currentHP = initHP; // Initialize current HP
        playerController = GetComponent<PlayerController>();
        playerController.UpdateHPDisplay(currentHP);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        SoundManager.instance.PlaySoundhurt();
        playerController.UpdateHPDisplay(currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle the player's death here
        Debug.Log("Player has died!");
        Gameover();
    }

    void Gameover()
    {
        SoundManager.instance.PlaySoundDie();
        GameoverController.instance.Show();
    }
}
