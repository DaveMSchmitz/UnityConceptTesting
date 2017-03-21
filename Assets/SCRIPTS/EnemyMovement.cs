using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovementController {

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
        

        bool shouldJump = false;

        if (_controller.Collision.right)
        {
            Debug.Log(_controller.Collision.rightGameObject);
            
            shouldJump = _controller.Collision.rightGameObject.layer == LayerMask.NameToLayer("Ground");
        }

        if (_controller.Collision.left)
        {
            Debug.Log(_controller.Collision.leftGameObject);
            shouldJump = _controller.Collision.leftGameObject.layer == LayerMask.NameToLayer("Ground");
        }

        return shouldJump;
        
    }

   
}
