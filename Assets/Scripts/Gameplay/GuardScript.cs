using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardScript : MonoBehaviour
{

    [SerializeField] private int _rotationSpeed, _degreeRange, _laserLength;
    private float _startRotationValue;

    [SerializeField] private MainCharacter _mainCharacter;

    private void Start()
    {
        this._startRotationValue = this.transform.rotation.eulerAngles.z;

        // TODO
        // Scale light cone according to laser length

        GlobalManager.StartRun();
    }

    private void Update()
    {
        this.LookAround();
    }

    private void LookAround()
    {
        this.Rotate();
        this.Detect();
    }

    private void Rotate()
    {
        float zValue = this._startRotationValue + Mathf.PingPong(Time.time * this._rotationSpeed, this._degreeRange) - (this._degreeRange / 2);
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

}
