using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "Game")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GlobalManager.LoadMainMenu();
            }
        }
        else if (activeScene == "Leaderboard")
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GlobalManager.LoadMainMenu();
            }
        }
        else if (activeScene == "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GlobalManager.LoadLeaderboard();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GlobalManager.LoadGame();
            }
        }
        else
        {
            Debug.Log("Unknown active scene: " + activeScene);
        }
    }
}
