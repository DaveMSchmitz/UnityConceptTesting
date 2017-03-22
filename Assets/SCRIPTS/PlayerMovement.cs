using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MovementController
{

    public override float getHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool getJump() {
        return Input.GetButton("Jump");
    }
}
