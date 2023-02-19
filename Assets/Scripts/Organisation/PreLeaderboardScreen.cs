using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PreLeaderboardScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInputField;
    [SerializeField] private LeaderBoardController _leaderBoardController;
    private Canvas _canvas;
    [SerializeField] private Canvas _leaderBoardCanvas;

    void Start()
    {
        this._canvas = GetComponent<Canvas>();

        if (GlobalManager.IsGameSolved)
        {
            this._leaderBoardCanvas.gameObject.SetActive(false);
        }
        else
        {
            this._leaderBoardCanvas.gameObject.SetActive(true);
            this._canvas.gameObject.SetActive(false);
        }

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && this._playerNameInputField.text != "") 
        {
            SetPlayerName();
            this._leaderBoardController.SubmitScore();
            StartCoroutine(ChangeLeaderboard());
        }
    }

    private IEnumerator ChangeLeaderboard()
    {
        yield return new WaitForSeconds(0.5f);

        this._leaderBoardController.ShowScores();
        this._leaderBoardCanvas.gameObject.SetActive(true);
        this._canvas.gameObject.SetActive(false);
    }

    public void SetPlayerName()
    {
        GlobalManager.PlayerName = _playerNameInputField.text;
    }
}
