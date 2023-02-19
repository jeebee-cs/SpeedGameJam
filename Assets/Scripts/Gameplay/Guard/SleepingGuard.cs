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

    private float _targetVisionIntensity, _targetVisionRadius;

    private SpriteChanger _spriteChanger;

    protected override void Start()
    {
        this._targetVisionIntensity = base.Vision.intensity;
        this._targetVisionRadius = base.Vision.pointLightOuterRadius;
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
            base.Vision.intensity = 0;
            this._spriteChanger.IncreaseSize();
            base.LaserLength = 0f;
            base.ScaleLightCone();
        }
        else
        {
            base.LaserLength = base.Vision.pointLightOuterRadius;
            this._spriteChanger.MakeInvisible();
            this.AdaptLightToCycle();
            base.Update();
        }
    }

    private void AdaptLightToCycle()
    {
        float timeLeft = this._sleepingIntervallTime - (Time.time - this._sleepingCycleStart);

        float normed = timeLeft / this._sleepingIntervallTime;

        float shifted = normed - 0.5f;

        float absolute = Mathf.Abs(shifted);

        absolute = absolute < 0.1f ? 0.1f : absolute;

        base.Vision.intensity = this._targetVisionIntensity / (1 + (absolute * this._targetVisionIntensity));
        base.Vision.pointLightOuterRadius = this._targetVisionRadius / (1 + (absolute * this._targetVisionRadius));
    }
}
 