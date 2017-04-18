using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

    public float MaxSpeed = 8f;
    public float JumpSpeed = 12f;

    public bool PlatformRelativeJump = false;

    MovingPlatform _movingPlatform;
    Rigidbody2D rbody2D;
    AIController _ai;
    Animator _animator;
    bool _groundedLastFrame;
    bool _jumping;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        rbody2D = GetComponent<Rigidbody2D>();
        _ai = GetComponent<AIController>();

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "MovingPlatform") {
            _movingPlatform = col.gameObject.GetComponent<MovingPlatform>();
        }
    }

    bool IsGrounded() {
        bool result = false;

        if (Mathf.Abs(RelativeVelocity().y) < 0.1f) {

            if (_groundedLastFrame) {
                result = true;
            }

            _groundedLastFrame = true;

        } else {
            _groundedLastFrame = false;
        }

        return result;
    }

    Vector2 PlatformVelocity() {
        Vector2 result = Vector2.zero;

        if (_movingPlatform != null) {
            result = _movingPlatform.GetComponent<Rigidbody>().velocity;
        }

        return result;
    }

    Vector2 RelativeVelocity() {
        return rbody2D.velocity - PlatformVelocity();
    }

    void FixedUpdate() {
        bool isGrounded = IsGrounded();

        if (_movingPlatform != null && !_groundedLastFrame && !isGrounded) {
            _movingPlatform = null;
        }

        float xVelocity = rbody2D.velocity.x;
        float yVelocity = rbody2D.velocity.y;

        if (isGrounded) {
            yVelocity = PlatformVelocity().y - 0.01f;
        }

        xVelocity = _ai.GetMovementFromPlayer() * MaxSpeed;
        xVelocity += PlatformVelocity().x;

        if (_ai.GetJump() && isGrounded) {
            yVelocity = JumpSpeed;

            if (PlatformRelativeJump) {
                yVelocity += PlatformVelocity().y;
            }
        }

        
        rbody2D.velocity = new Vector2(xVelocity, yVelocity);

        if (_ai.GetMovementFromPlayer() != 0) {
            transform.localScale = new Vector3(_ai.GetMovementFromPlayer(), transform.localScale.y, transform.localScale.z);
        }

        _animator.SetBool("onGround", isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(RelativeVelocity().x));
    }
}
