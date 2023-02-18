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
    private static string s_playerName = "anon";

    public static string PlayerName
    {
        get { return s_playerName; }
        set { s_playerName = value; }
    }

    #endregion

    #region GameManagement

    // GOLDSTACKS
    private static int s_numberOfGoldstacks = 0;
    private static int s_collectedGoldstacks = 0;

    public static void InitializeGoldstack()
    {
        s_numberOfGoldstacks++;
    }

    public static void CollectGoldstack()
    {
        Debug.Log("Collected gold");
        s_collectedGoldstacks++;
    }


    // GAME ENDED SUCCESSFULLY
    private static bool s_isGameSolved = false;

    public static bool IsGameSolved
    {
        get 
        {
            Debug.Log(s_numberOfGoldstacks);
            Debug.Log(s_collectedGoldstacks);
            if (s_numberOfGoldstacks > 0) return s_numberOfGoldstacks == s_collectedGoldstacks;
            else return false;
        }
    }

    // SCORE CALCULATION
    private static float s_startTime;

    public static void StartRun()
    {
        s_startTime = Time.time;
    }

    public static void SolvedGame()
    {
        s_score = Time.time - s_startTime;
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
