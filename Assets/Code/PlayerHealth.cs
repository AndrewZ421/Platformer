using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int initHP = 3; // Initial health points
    public int currentHP; // Current health points
    public PlayerController playerController;
    private bool isTestMode = false; // Flag for test mode

    void Start()
    {
        currentHP = initHP; // Initialize current HP
        playerController = GetComponent<PlayerController>();
        playerController.UpdateHPDisplay(currentHP);
    }

    void Update()
    {
        // Check if the test mode key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTestMode();
        }
    }

    private void ToggleTestMode()
    {
        isTestMode = !isTestMode; // Toggle test mode state
        if (isTestMode)
        {
            currentHP = 99; // Set high health value
        }
        else
        {
            currentHP = initHP; // Reset to initial health value
        }
        playerController.UpdateHPDisplay(currentHP); // Update display
    }

    public void TakeDamage(int damage)
    {
        if (!isTestMode) // If not in test mode
        {
            currentHP -= damage;
            SoundManager.instance.PlaySoundhurt();
            playerController.UpdateHPDisplay(currentHP);
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Handle player's death logic
        Debug.Log("Player has died!");
        Gameover();
    }

    void Gameover()
    {
        SoundManager.instance.PlaySoundDie();
        GameoverController.instance.Show();
    }
}
