using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool _isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this._isPaused)
            {
                GlobalManager.LoadMainMenu();
            }

            Time.timeScale = 0;
            this._isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this._isPaused = false;
            Time.timeScale = 1;
        }
    }
}
