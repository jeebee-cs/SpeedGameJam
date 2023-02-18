using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardScript : MonoBehaviour
{

    [SerializeField] private int _rotationSpeed, _degreeRange;

    [SerializeField] private float _laserLength;
    public float LaserLength
    {
        get { return this._laserLength; }
        set { this._laserLength = value; }
    }

    private float _startRotationValue;

    private float _internalTime = 0f;

    [SerializeField] private MainCharacter _mainCharacter;

    protected virtual void Start()
    {
        this._startRotationValue = this.transform.rotation.eulerAngles.z;

        this.ScaleLightCone();
    }

    protected void ScaleLightCone()
    {
        // Scale light cone according to laser length
        Transform lightCone = this.transform.GetChild(0);
        lightCone.localPosition = new Vector3(this._laserLength, 0, 0);
        float parentScalingFactor = 1 / this.transform.localScale.x;
        lightCone.localScale = new Vector3(this._laserLength * parentScalingFactor, 1, 1);
    }

    protected virtual void Update()
    {
        // need to use internal time so that sleeping guard works smoothly
        this._internalTime += Time.deltaTime;
        this.LookAround();
    }

    #region Looking Around

    private void LookAround()
    {
        this.Rotate();
        this.Detect();
    }

    private void Rotate()
    {
        float zValue = this._startRotationValue + Mathf.PingPong(this._internalTime * this._rotationSpeed, this._degreeRange) - (this._degreeRange / 2);
        this.transform.localEulerAngles = new Vector3(0, 0, zValue);
    }

    private void Detect()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, this._laserLength);

        //If the collider of the object hit is not NUll
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                this._mainCharacter.Die();
            }
        }

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(transform.position, this.transform.right * this._laserLength, Color.red);
    }
    #endregion

}
