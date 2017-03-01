using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Focus;
    public float CameraBuffer;
    public float Smoothness;
    public float ClampYPosition;
    public bool FollowFocusAlongX;
    public bool FollowFocusAlongY;

    private Vector3 focusPosition;

    // Update is called once per frame
    void Update() {

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;


        if (FollowFocusAlongX) {
            x = Focus.transform.position.x + Mathf.Sign(Focus.transform.localScale.x) * CameraBuffer;

        }

        if (FollowFocusAlongY) {
            y = Mathf.Clamp(Focus.transform.position.y, ClampYPosition, float.MaxValue);

        }

        focusPosition = new Vector3(x, y, z);
        transform.position = Vector3.Lerp(transform.position, focusPosition, Smoothness * Time.deltaTime);
    }
}
