using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalManager
{

    #region Leaderboard Relevant

    // SCORE
    private static float s_score;
    
    public static float Score
    {
        get { return s_score; }
        set { s_score = value; } // will later be replaced by internal score setting (startimte - endtime)
    }

    // PLAYER NAME
    private static string s_playerName = "debug";

    public static string PlayerName
    {
        get { return s_playerName; }
        set { s_playerName = value; }
    }

    #endregion

    #region GameManagement

    // GAME SOLVED
    private static bool s_isGameSolved = false;
    
    public static bool IsGameSolved
    {
        get { return s_isGameSolved; }
    }

    // SETTING SCORE
    public static void SolvedGame(float score)
    {
        s_score = score;
        s_isGameSolved = true;
        LoadLeaderboard();
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
