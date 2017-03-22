using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PhysicsObject {

    public float jumpTakeOffSpeed = 15;
    public float MaxSpeed = 10;

    private ParticleSystem _jumpParticle;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // Use this for initialization
    void Awake () {
        _jumpParticle = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponent<Animator>();
	}
	
    protected override void ComputeVelocity() {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        

        if(Input.GetButton("Jump") && isGrounded) {
            velocity.y = jumpTakeOffSpeed;
            _jumpParticle.Play();
        }else if (Input.GetButtonUp("Jump")) {

            if(velocity.y > 0) {
                velocity.y = velocity.y * .5f;
            }
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
