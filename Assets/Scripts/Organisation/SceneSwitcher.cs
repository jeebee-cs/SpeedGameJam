using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GlobalManager.LoadMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GlobalManager.LoadLeaderboard();
        }
    }
}
