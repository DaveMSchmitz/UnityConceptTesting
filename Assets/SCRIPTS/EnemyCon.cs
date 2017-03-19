using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : Movement {

    private AIController _ai;
   
    public override void SetUp()
    {
        Debug.Log("in start up");
        _ai = GetComponentInChildren<AIController>();
        _ai.foo();
        
    }

    public override float getHorizontal()
    {
        //return 0;
        return GetComponentInChildren<AIController>().GetMovement();
    }

    public override bool getJump()
    {
        return base._controller.Collision.right || base._controller.Collision.left;
    }
}
