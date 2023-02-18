using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI _timerText;
  [SerializeField] MainCharacter _mainCharacter;
  RealTimeTimer _timer;
  void Start()
  {
    _timer = new RealTimeTimer(300, Time.realtimeSinceStartup);
  }
  void Update()
  {
    if (_timer.IsOver()) _mainCharacter.Die();
    _timerText.text = _timer.HumanReadable();
  }
}
