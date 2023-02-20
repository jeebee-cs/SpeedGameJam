using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
  MusicManager _musicManager;
  [SerializeField] AudioClip _gameMusic;
  [SerializeField] AudioClip _menuMusic;
  private void Awake() {
    _musicManager = GameObject.Find("Music").GetComponent<MusicManager>();
  }
  void Update()
  {
    string activeScene = SceneManager.GetActiveScene().name;

    if (activeScene == "Game")
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        _musicManager.ChangeMusic(_menuMusic);
        GlobalManager.LoadMainMenu();
      }
    }
    else if (activeScene == "Leaderboard")
    {
        _musicManager.ChangeMusic(_menuMusic);
      if (Input.GetKeyDown(KeyCode.Backspace))
      {
        GlobalManager.LoadMainMenu();
      }
    }
    else if (activeScene == "MainMenu")
    {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
        _musicManager.ChangeMusic(_menuMusic);
        GlobalManager.LoadLeaderboard();
      }

      if (Input.GetKeyDown(KeyCode.Space))
      {
        _musicManager.ChangeMusic(_gameMusic);
        GlobalManager.LoadGame();
      }
    }
    else
    {
      Debug.Log("Unknown active scene: " + activeScene);
    }
  }
}
