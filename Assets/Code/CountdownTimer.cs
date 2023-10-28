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

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        if (isGameOver)
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

    void EndGame()
    {
        SoundManager.instance.PlaySoundDie();
        GameoverController.instance.Show();
    }
}