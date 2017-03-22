using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovementController {

    public GameObject PlayerSensor;

    private AIController _ai;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        _ai = GetComponentInChildren<AIController>();
    }


    public override float getHorizontal()
    {
        
        return _ai.GetMovement();
    }

    public override bool getJump()
    {
        return _controller.Collision.right || _controller.Collision.left;
    }

   
}
