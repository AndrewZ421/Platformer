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
            if (SceneManager.GetActiveScene().name == "level1")
            {
                textLevel.text = "Level 1";
            }
            else if (SceneManager.GetActiveScene().name == "level2")
            {
                textLevel.text = "Level 2";
            }
            else if (SceneManager.GetActiveScene().name == "level3")
            {
                textLevel.text = "Level 3";
            }
        }
    }
}