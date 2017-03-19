using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Movement
{
    public override float getHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool getJump() {
        return Input.GetButtonDown("Jump");
    }
}
