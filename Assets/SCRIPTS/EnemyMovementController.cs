using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : PhysicsObject {

    public float jumpTakeOffSpeed = 12;
    public float MaxSpeed = 8;


    private Animator _animator;
    private AIController _ai;

    void Awake() {
        
        _animator = GetComponent<Animator>();
        _ai = GetComponentInChildren<AIController>();
    }


    // Update is called once per frame
    protected override void ComputeVelocity() {
        Vector2 move = Vector2.zero;

        move.x = _ai.GetMovementFromPlayer();
        //move.x = Input.GetAxis("Horizontal");


        if (_ai.GetJump() && isGrounded) {
            velocity.y = jumpTakeOffSpeed;
        } 

        

        if (move.x != 0) {
            transform.localScale = new Vector3(Mathf.Sign(move.x), transform.localScale.y);
        }

        targetVelocity = move * MaxSpeed;

                
    }

    void LateUpdate() {
        _animator.SetBool("onGround", isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(velocity.x));
    }
}
