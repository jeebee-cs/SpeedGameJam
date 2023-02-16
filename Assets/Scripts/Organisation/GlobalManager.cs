using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalManager
{

    #region Fields and Properties

    // SCORE
    private static float s_score;
    
    public static float Score
    {
        get { return s_score; }
        set { s_score = value; } // will later be replaced by internal score setting (startimte - endtime)
    }

    // PLAYER NAME
    private static string s_playerName = "anon";

    public static string PlayerName
    {
        get { return s_playerName; }
        set {s_playerName = value; }
    }

    // GAME ENDED SUCCESSFULLY
    private static bool s_isGameSolved = false;

    public static bool IsGameSolved
    {
        get { return s_isGameSolved; }
        set { s_isGameSolved = value; }
    }

    #endregion

    #region SceneManagement
    
    public static void LoadMainMenu()
    {
        Load("MainMenu");
    }

    public static void LoadLeaderboard()
    {
        Load("Leaderboard");
    }

    public static void LoadGame()
    {
        Load("Game");
    }

    private static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    #endregion
}
