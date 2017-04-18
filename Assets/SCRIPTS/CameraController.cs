using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Renderer target;
    public Rect box;
    public Vector3 pos;

    

    protected Camera _camera;
    protected Vector3 _cureentVelocity;


    public void Start() {

        //set the initial position to where the target is
        pos = target.transform.position;
        pos.z = transform.position.z;

        //zero the current velocity
        _cureentVelocity = Vector3.zero;

    }

    public void Update() {

        //we first figure out where the target is relative to the camera itself
        float localX = target.transform.position.x - transform.position.x;
        float localY = target.transform.position.y - transform.position.y;

        //if the target is outside of the box area to the left then move the camera so that it
        //is inside of the box area
        if (localX < box.xMin) {
            pos.x += localX - box.xMin;

            //if the target is outside of the box area to the right then move the camera so that it
            //is inside of the box area
        } else if (localX > box.xMax) {
            pos.x += localX - box.xMax;

         //if the target is outside of the box area to the bottom then move the camera so that it
         //is inside of the box area
        }
        if (localY < box.yMin) {
            pos.y += localY - box.yMin;

            //if the target is outside of the box area to the top then move the camera so that it
            //is inside of the box area
        } else if (localY > box.yMax) {
            pos.y += localY - box.yMax;

        }

        transform.position = pos;
       
    }

}
