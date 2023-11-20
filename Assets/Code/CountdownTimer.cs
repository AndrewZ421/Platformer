using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Platformer;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 20.0f;
    private float currentTime;
    public TextMeshProUGUI countdownText;
    private bool isGameOver = false;
    private bool isTestMode = false; // Flag for test mode

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        // Check if the test mode key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTestMode();
        }

        if (isGameOver || isTestMode) // Pause timer if game is over or in test mode
        {
            return;
        }

        currentTime -= Time.deltaTime;

        countdownText.text = "Time Left: " + currentTime.ToString("F2");

        if (currentTime <= 0)
        {
            currentTime = 0;
            countdownText.text = "Time Left: 0.00";
            isGameOver = true;
            EndGame();
        }
    }

    private void ToggleTestMode()
    {
        isTestMode = !isTestMode; // Toggle test mode state
    }

    void EndGame()
    {
        SoundManager.instance.PlaySoundDie();
        GameoverController.instance.Show();
    }
}
