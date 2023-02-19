using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoxScaler : MonoBehaviour
{
    //the collider that will be adjusted to fit the Camera

    private EdgeCollider2D _col;

    // the camera the collider is adjusted to

    [SerializeField] private Camera _cam;


    private void Start()
    {
        this._col = GetComponent<EdgeCollider2D>();

        //Initializes Array with 5 Corners(5Because: reconnecting to first position)

        Vector2[] points = new Vector2[5];

        //sets all the postions where the points need to be

        
        Vector2 screenSize = new Vector2(Screen.width / 2, Screen.height / 2);
        
        /*
        points[0] = this._cam.ScreenToWorldPoint(new Vector3(screenSize.x, screenSize.y, 0)) + this._cam.transform.position;
        points[1] = this._cam.ScreenToWorldPoint(new Vector3(screenSize.x, -screenSize.y, 0)) + this._cam.transform.position;
        points[2] = this._cam.ScreenToWorldPoint(new Vector3(-screenSize.x, -screenSize.y, 0)) + this._cam.transform.position;
        points[3] = this._cam.ScreenToWorldPoint(new Vector3(-screenSize.x, screenSize.y, 0)) + this._cam.transform.position;
        */

        points[0] = this._cam.ViewportToWorldPoint(new Vector2(0.975f, 0.975f)) - this._cam.transform.position;
        points[1] = this._cam.ViewportToWorldPoint(new Vector2(0.025f, 0.975f)) - this._cam.transform.position;
        points[2] = this._cam.ViewportToWorldPoint(new Vector2(0.025f, 0.025f)) - this._cam.transform.position;
        points[3] = this._cam.ViewportToWorldPoint(new Vector2(0.975f, 0.025f)) - this._cam.transform.position;
        points[4] = this._cam.ViewportToWorldPoint(new Vector2(0.975f, 0.975f)) - this._cam.transform.position;


        //sets all the points of the collider to the new positions

        this._col.points = points;
    }
}