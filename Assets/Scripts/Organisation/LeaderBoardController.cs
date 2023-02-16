using UnityEngine.UI;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderBoardController : MonoBehaviour
{
    private int ID = 11807;
    private int maxScores = 3;
    public TMP_Text[] leaderboardTexts;

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
                    leaderboardTexts[i].text = (scores[i].rank + ".  " + scores[i].member_id + ": " + scores[i].score);
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        leaderboardTexts[i].text = (i + 1).ToString() + ". --- ";
                    }
                }

            }
            else
            {
                Debug.Log("Failed to show leaderboard.");
            }
        });
    }
}
