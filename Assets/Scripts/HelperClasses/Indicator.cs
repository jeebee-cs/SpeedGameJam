using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;
    public GameObject IndicatorGameObject
    {
        get { return _indicator; }
    }

    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _layerToHit;

    protected virtual void Update()
    {
        Vector2 direction = this._target.transform.position - this.transform.position;

        float length = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, length, this._layerToHit); ;

        // rotate triangle so that it points to target
        this._indicator.transform.up = this._target.position - this._indicator.transform.position;

        if (hit.collider != null)
        {
            // set indicator to collision between ray and camera box
            this._indicator.transform.position = hit.point;
        }

        Debug.DrawRay(transform.position, direction, Color.red);
    }
}
