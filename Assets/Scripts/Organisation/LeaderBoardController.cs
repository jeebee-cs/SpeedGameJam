using UnityEngine.UI;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderBoardController : MonoBehaviour
{
  private int ID = 11807;
  private int maxScores = 3;
  public TMP_Text[] leaderboardTextsScore;
  public TMP_Text[] leaderboardTextsName;

  private void Start()
  {
    LootLockerSDKManager.StartGuestSession((response) =>
    {
      if (response.success)
      {
        if (GlobalManager.IsGameSolved)
        {
          SubmitScore();
        }

        ShowScores();
      }
      else
      {
        Debug.Log("Starting LootLocker Session failed.");
      }
    });
  }

  private void SubmitScore()
  {
    Debug.Log(GlobalManager.Score);
    LootLockerSDKManager.SubmitScore(GlobalManager.PlayerName, (int)(GlobalManager.Score), ID, (response) =>
    {
      if (!response.success)
      {
        Debug.Log("Failed to submit score.");
      }

    });
  }

  public void ShowScores()
  {
    LootLockerSDKManager.GetScoreList(ID, maxScores, 0, (response) =>
    {
      if (response.success)
      {
        LootLockerLeaderboardMember[] scores = response.items;

        for (int i = 0; i < scores.Length; i++)
        {
          float score = scores[i].score / 100f;

          float minutes = Mathf.Floor(score / 60);
          float seconds = Mathf.Floor(score % 60);
          float miliseconds = Mathf.Floor((score % 1) * 100);

          string scoreString =  minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00");

          leaderboardTextsScore[i].text = scoreString;
          leaderboardTextsName[i].text = scores[i].member_id;
        }

      }
      else
      {
        Debug.Log("Failed to show leaderboard.");
      }
    });
  }
}
