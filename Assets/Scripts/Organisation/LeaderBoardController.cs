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
  public TMP_Text yourRanking;

  void Start()
  {
    LootLockerSDKManager.StartGuestSession((response) =>
    {
      if (!response.success)
      {
        Debug.Log("Starting LootLocker Session failed.");


      }
        else
        {
            if (!GlobalManager.IsGameSolved)
            {
                this.ShowScores();
            }
        }
    });
  }

  public void SubmitScore()
  {
    LootLockerSDKManager.SubmitScore(GlobalManager.PlayerName, (int)(GlobalManager.Score), ID, (response) =>
    {
      if (!response.success)
      {
        Debug.Log("Failed to submit score.");
      }

    });
  }

  private void GetRank()
    {
        LootLockerSDKManager.GetMemberRank(ID, GlobalManager.PlayerName, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log(response.text);

                this.yourRanking.text = "YOUR RANK: " + response.rank.ToString();
            }
            else
            {
                Debug.Log("failed: " + response.Error);
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

        if (GlobalManager.IsGameSolved)
            {
                this.GetRank();
            }
        }
        else
      {
        Debug.Log("Failed to show leaderboard.");
      }
    });
  }

}
