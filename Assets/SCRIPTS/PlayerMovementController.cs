using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float MaxSpeed = 10f;
    public float DefaultMaxSpeed;
    public float JumpSpeed = 12f;
    public Transform JumpSensor;
    public float Radius;
    public LayerMask ConsideredGround;

    public bool PlatformRelativeJump = false;

    MovingPlatform _movingPlatform;
    Rigidbody2D rbody2D;
    Animator _animator;
    bool _jumping;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        rbody2D = GetComponent<Rigidbody2D>();
        DefaultMaxSpeed = MaxSpeed;
    }

    public void multSpeed(float coef) {
        MaxSpeed *= coef;
    }

    public void restoreSpeed() {
        MaxSpeed = DefaultMaxSpeed;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "MovingPlatform") {
            _movingPlatform = col.gameObject.GetComponent<MovingPlatform>();

        }
    }

    bool IsGrounded() {

        return Physics2D.OverlapCircle(JumpSensor.position, Radius, ConsideredGround);
    }

    Vector2 PlatformVelocity() {
        Vector2 result = Vector2.zero;

        if (_movingPlatform != null) {
            result = _movingPlatform.GetComponent<Rigidbody2D>().velocity;
        }

        return result;
    }

    Vector2 RelativeVelocity() {
        return rbody2D.velocity - PlatformVelocity();
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Jump")) {
            _jumping = true;
        }
    }

    void FixedUpdate() {
        bool isGrounded = IsGrounded();

        if (_movingPlatform != null && !isGrounded) {
            _movingPlatform = null;
        }

        float xVelocity = rbody2D.velocity.x;
        float yVelocity = rbody2D.velocity.y;

        if (isGrounded && rbody2D.velocity.y < 0 && _movingPlatform != null) {
            yVelocity = PlatformVelocity().y - 0.01f;

        }

        xVelocity = Input.GetAxis("Horizontal") * MaxSpeed;


        xVelocity += PlatformVelocity().x;



        if (_jumping && isGrounded) {
            yVelocity = JumpSpeed;

            if (PlatformRelativeJump) {
                yVelocity += PlatformVelocity().y;
            }
        }

        _jumping = false;

        rbody2D.velocity = new Vector2(xVelocity, yVelocity);

        if (Input.GetAxisRaw("Horizontal") != 0) {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), transform.localScale.y, transform.localScale.z);
        }

        _animator.SetBool("onGround", isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(RelativeVelocity().x));
    }
}
