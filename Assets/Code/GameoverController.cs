using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameoverController : MonoBehaviour
    {
        public static GameoverController instance;

        // Outlets
        public GameObject gameoverMenu;
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
            gameoverMenu.SetActive(false);
            levelMenu.SetActive(false);

            // Turn on requested menu
            someMenu.SetActive(true);
        }

        public void ShowGameoverMenu()
        {
            SwitchMenu(gameoverMenu);
        }

        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Show()
        {
            ShowGameoverMenu();
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

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
