using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Controller2D Player;
    public Vector2 BoxAreaSize;
    public float verticalOffset;
    public float horizontalOffset;
    public float verticalSmooth;
    public float horizontalSmooth;

    private BoxArea box;



    void Start()
    {
     box = new BoxArea(Player._collider.bounds, BoxAreaSize);   
    }

    private struct BoxArea
    {
        public Vector2 center;
        float left, right;
        float top, bottom;

        public BoxArea(Bounds playerBounds, Vector2 size)
        {
            left = playerBounds.center.x - size.x / 2;
            right = playerBounds.center.x + size.x / 2;
            bottom = playerBounds.min.y;
            top = playerBounds.min.y + size.y;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds playerBounds)
        {
            float xMove = 0;
            float yMove = 0;

            if (playerBounds.min.x < left)
            {
                xMove = playerBounds.min.x - left;
            }
            else if (playerBounds.max.x > right)
            {
                xMove = playerBounds.max.x - right;
            }


            if (playerBounds.min.y < bottom)
            {
                yMove = playerBounds.min.y - bottom;
            }
            else if (playerBounds.max.y > top)
            {
                yMove = playerBounds.max.y - top;
            }

            left += xMove;
            right += xMove;
            top += yMove;
            bottom += yMove;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        box.Update(Player._collider.bounds);
        Vector2 focus = box.center + Vector2.up + new Vector2(0,verticalOffset);
        Vector3 position = (Vector3) focus;
        position.z = -10;
        transform.position = position;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,0,1,.2f);
        Gizmos.DrawCube(box.center, BoxAreaSize);
    }



        
}
