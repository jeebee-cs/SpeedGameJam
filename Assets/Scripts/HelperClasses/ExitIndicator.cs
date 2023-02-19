using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitIndicator : Indicator
{
    void Start()
    {
        base.IndicatorGameObject.SetActive(false);
    }

    protected override void Update()
    {
        if (GlobalManager.IsAllGoldCollected)
        {
            if (!base.IndicatorGameObject.activeSelf)
            {
                base.IndicatorGameObject.SetActive(true);
            }

            base.Update();
        }
    }
}
