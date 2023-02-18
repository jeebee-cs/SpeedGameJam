using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
  float _timerStarted;
  public float timerStarted { get => _timerStarted; }
  float _duration;
  public float duration { get => _duration; set => _duration = value; }
  bool _isFinished;
  public bool isFinished { get => _isFinished; set => _isFinished = value; }

  public Timer(float duration, float timerStarted = 0)
  {
    _duration = duration;
    _timerStarted = timerStarted;
  }

  /// <summary>
  /// Timer that loop when it's over
  /// </summary>
  /// <returns>If the timer is over or not</returns>
  public bool IsOverLoop()
  {
    bool isOver = this.IsOver();
    if (isOver) Reset();
    return isOver;
  }
  /// <summary>
  /// Is over?
  /// </summary>
  /// <returns>If the timer is over or not</returns>
  public bool IsOver()
  {
    if (!_isFinished) _isFinished = Time.time >= _timerStarted + _duration;
    return _isFinished;
  }

  public void Reset()
  {
    _isFinished = false;
    _timerStarted = Time.time;
  }

  public float PercentTime(float time = 0)
  {
    if (_isFinished) return 1;
    time += Time.time;
    float deltaTime = time - _timerStarted;
    float percent = Matho.Percent(deltaTime, _duration);
    return Mathf.Clamp(percent, 0, 1);
  }
  public float TimeLeft()
  {
    if (_isFinished) return 0;
    return _duration - TimeDone();
  }
  public float TimeDone()
  {
    if (_isFinished) return duration;
    return Time.time - _timerStarted;
  }
  public void Finish()
  {
    _isFinished = true;
  }

  public string HumanReadable()
  {
    float minutes = Mathf.Floor(TimeLeft() / 60);
    float seconds = Mathf.Floor(TimeLeft() % 60);
    float miliseconds = Mathf.Floor((TimeLeft() % 1) * 100);

    return minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00");
  }
}
[System.Serializable]
public class RealTimeTimer
{
  float _timerStarted;
  public float timerStarted { get => _timerStarted; }
  float _duration;
  public float duration { get => _duration; set => _duration = value; }
  bool _isFinished;
  public bool isFinished { get => _isFinished; set => _isFinished = value; }

  public RealTimeTimer(float duration, float timerStarted = 0)
  {
    _duration = duration;
    _timerStarted = timerStarted;
  }

  /// <summary>
  /// Timer that loop when it's over
  /// </summary>
  /// <returns>If the timer is over or not</returns>
  public bool IsOverLoop()
  {
    bool isOver = this.IsOver();
    if (isOver) Reset();
    return isOver;
  }
  /// <summary>
  /// Is over?
  /// </summary>
  /// <returns>If the timer is over or not</returns>
  public bool IsOver()
  {
    if (!_isFinished) _isFinished = Time.realtimeSinceStartup >= _timerStarted + _duration;
    return _isFinished;
  }

  public void Reset()
  {
    _isFinished = false;
    _timerStarted = Time.realtimeSinceStartup;
  }

  public float PercentTime(float time = 0)
  {
    if (_isFinished) return 1;
    time += Time.realtimeSinceStartup;
    float deltaTime = time - _timerStarted;
    float percent = Matho.Percent(deltaTime, _duration);
    return Mathf.Clamp(percent, 0, 1);
  }
  public float TimeLeft()
  {
    if (_isFinished) return 0;
    return _duration - TimeDone();
  }
  public float TimeDone()
  {
    if (_isFinished) return duration;
    return Time.realtimeSinceStartup - _timerStarted;
  }
  public void Finish()
  {
    _isFinished = true;
  }
  public string HumanReadable()
  {
    float minutes = Mathf.Floor(TimeLeft() / 60);
    float seconds = Mathf.Floor(TimeLeft() % 60);
    float miliseconds = Mathf.Floor((TimeLeft() % 1) * 100);

    return minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miliseconds.ToString("00");
  }
}
