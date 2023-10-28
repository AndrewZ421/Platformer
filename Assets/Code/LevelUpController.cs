using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class LevelUpController : MonoBehaviour
    {
        public static LevelUpController instance;

        // Outlets
        public GameObject levelUpMenu;
        public GameObject levelMenu;

        // Methods
        void Awake()
        {
            instance = this;
            Hide();
        }

        void SwitchMenu(GameObject someMenu)
        {
            // Clean-up Menus
            levelUpMenu.SetActive(false);
            levelMenu.SetActive(false);

            // Turn on requested menu
            someMenu.SetActive(true);
        }

        public void ShowLevelUpMenu()
        {
            SwitchMenu(levelUpMenu);
        }

        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Show()
        {
            ShowLevelUpMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (PlayerController.instance != null)
            {
                PlayerController.instance.isPaused = false;
            }
        }

        public void LoadLevel(string levelName)
        {
            Debug.Log(levelName);
            SceneManager.LoadScene(levelName);
            GameController.instance.UpdateTextLevel();
        }

        public void LoadNextLevel(string nextLevelName)
        {
            Debug.Log(nextLevelName);
            SceneManager.LoadScene(nextLevelName);
            GameController.instance.UpdateTextLevel();
        }
    }
}
