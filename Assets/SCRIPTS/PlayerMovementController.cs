using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float MaxSpeed = 10f;
    public float JumpSpeed = 12f;

    public bool PlatformRelativeJump = false;

    MovingPlatform _movingPlatform;
    Rigidbody2D rigidbody2D;
    Animator _animator;
    bool _groundedLastFrame;
    bool _jumping;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        
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
        return rigidbody2D.velocity - PlatformVelocity();
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump")) {
            _jumping = true;
        }
	}

    void FixedUpdate() {
        bool isGrounded = IsGrounded();

        if (_movingPlatform != null && !_groundedLastFrame && !isGrounded) {
            _movingPlatform = null;
        }

        float xVelocity = rigidbody2D.velocity.x;
        float yVelocity = rigidbody2D.velocity.y;

        if (isGrounded) {
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

        rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

        if (Input.GetAxisRaw("Horizontal") != 0) {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), transform.localScale.y, transform.localScale.z);
        }

        _animator.SetBool("onGround", isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(RelativeVelocity().x));
    }
}
