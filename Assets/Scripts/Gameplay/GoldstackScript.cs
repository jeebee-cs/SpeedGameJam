using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldstackScript : MonoBehaviour
{
    void Awake()
    {
        GlobalManager.InitializeGoldstack();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GlobalManager.CollectGoldstack();
        }
    }
}
