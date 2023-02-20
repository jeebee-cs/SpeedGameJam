using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GuardScript : MonoBehaviour
{

    [SerializeField] private int _rotationSpeed, _degreeRange;

    [SerializeField] private Light2D _vision;
    [SerializeField] private LayerMask _layerToHit;
    public Light2D Vision
    {
        get { return _vision; }
        set { _vision = value; }
    }


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
        this._vision.pointLightOuterRadius = this._laserLength * 1.1f;
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

    protected void Detect()
    {
        Vector3[] directions = new Vector3[] { new Vector3(1, 0.15f, 0).normalized, new Vector3(1, 0, 0), new Vector3(1, -0.15f, 0).normalized };

        for (int i = 0; i < directions.Length; i++)
        {
            this.CastRayInAngleDirection(directions[i]);
        }
    }

    private void CastRayInAngleDirection(Vector3 direction)
    {
        direction = transform.rotation * direction;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, this._laserLength, _layerToHit);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                this._mainCharacter.Die();
            }
        }

        Debug.DrawRay(transform.position, direction * this._laserLength, Color.red);
    }

    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a");
        if (other.tag == "Player")
        {
            this.transform.right = this._mainCharacter.transform.position - this.transform.position;
            this._mainCharacter.Die();
        }
    }

}
