using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PatrolingGuard : GuardScript
{
    [SerializeField] private Transform[] _patrolingTargets;
    [SerializeField] private float _speed;
    private int _patrolingIndex;

    protected override void Start()
    {
        this._patrolingIndex = 0;
        base.Start();
    }

    protected override void Update()
    {
        Vector3 vecToTarget = this._patrolingTargets[this._patrolingIndex].position - this.transform.position;

        if (vecToTarget.sqrMagnitude < 0.05f) 
        {
            this._patrolingIndex++;
            this._patrolingIndex = this._patrolingIndex % this._patrolingTargets.Length;
            this.transform.right = this._patrolingTargets[this._patrolingIndex].position - this.transform.position;
        }

        this.MoveTowardsTarget();
        base.Detect();
    }

    private void MoveTowardsTarget()
    {
        float step = this._speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, this._patrolingTargets[this._patrolingIndex].position, step);
    }
}
