using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepingGuard : GuardScript
{
    [SerializeField] private bool _sleeping;

    [SerializeField] private float _sleepingIntervallTime;
    private float _sleepingCycleStart;

    private float _targetLaserLength;

    private SpriteChanger _spriteChanger;

    protected override void Start()
    {
        this._targetLaserLength = base.LaserLength;
        base.LaserLength = 0f;

        this._sleepingCycleStart = Time.time; // is that correct? How is the sleeping cycle gonna change when time is slower/faster?

        base.Start();

        this._spriteChanger = new SpriteChanger(this.transform.Find("SleepingIndicator").GetComponent<SpriteRenderer>());
    }

    protected override void Update()
    {
        if (this._sleepingIntervallTime < Time.time - this._sleepingCycleStart)
        {
            this._sleeping = !this._sleeping;
            this._sleepingCycleStart = Time.time;
            this._spriteChanger.ResetSprite();
        }

        if (this._sleeping)
        {
            this._spriteChanger.IncreaseSize();
            base.LaserLength = 0f;
            base.ScaleLightCone();
        }
        else
        {
            this._spriteChanger.MakeInvisible();
            // recalculate laserlength
            this.AdaptLaserLengthToCycle();
            base.ScaleLightCone();
            base.Update();
        }
    }

    private void AdaptLaserLengthToCycle()
    {
        float timeLeft = this._sleepingIntervallTime - (Time.time - this._sleepingCycleStart);
        Debug.Log("Time left in cycle: " + timeLeft.ToString());

        float normed = timeLeft / this._sleepingIntervallTime;
        
        Debug.Log("normed: " + normed.ToString());

        float shifted = normed - 0.5f;

        Debug.Log("Shifted: " + shifted.ToString());

        float absolute = Mathf.Abs(shifted);

        Debug.Log("Absolute: " + absolute.ToString());

        absolute = absolute < 0.1f ? 0.1f : absolute;

        base.LaserLength = this._targetLaserLength / (1 + (absolute* this._targetLaserLength));
    }

}
 