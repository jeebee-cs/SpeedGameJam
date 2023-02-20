using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  [SerializeField] AudioClip _music;
  [Range(0.0f, 100.0f)] public int _volume;
  AudioSource _audioSource;
  private static MusicManager _instance;
  public static MusicManager instance { get { return _instance; } }
  void Awake()
  {
    DontDestroyOnLoad(gameObject);
    _audioSource = gameObject.AddComponent<AudioSource>();

    _audioSource.clip = _music;
    _audioSource.loop = true;
    _audioSource.volume = _volume / 100f;

    _audioSource.Play();

    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
      return;
    }
    else
    {
      DontDestroyOnLoad(this);
      _instance = this;
    }
  }
  public void ChangeMusic(AudioClip otherMusic)
  {
    _music = otherMusic;
    _audioSource.clip = _music;
    _audioSource.Play();
  }
}
