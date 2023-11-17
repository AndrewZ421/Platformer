using Platformer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameController : MonoBehaviour
    {

        public static GameController instance;
        public TMP_Text textLevel;

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            UpdateTextLevel();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateTextLevel() {
            string currentLevelName = SceneManager.GetActiveScene().name;
            string levelPrefix = "level";

            int levelNumber;
            if (int.TryParse(currentLevelName.Substring(levelPrefix.Length), out levelNumber))
            {
                textLevel.text = "Level " + levelNumber;
            }
            else
            {
                Debug.LogError("Current level name does not contain a valid number: " + currentLevelName);
                textLevel.text = "Unknown Level";
            }
        }
    }
}