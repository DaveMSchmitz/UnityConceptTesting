using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MovementController
{

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = RespawnTransform;
        }
    }

    public override float getHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool getJump() {
        return Input.GetButton("Jump");
    }
}
