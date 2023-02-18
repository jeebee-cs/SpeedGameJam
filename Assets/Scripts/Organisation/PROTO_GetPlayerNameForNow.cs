using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PROTO_GetPlayerNameForNow : MonoBehaviour
{
    [SerializeField] TMP_InputField _playerNameInputField;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) { SetPlayerName(); }
    }

    public void SetPlayerName()
    {
        GlobalManager.PlayerName = _playerNameInputField.text;
    }
}
