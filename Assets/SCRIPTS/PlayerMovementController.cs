using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    //TODO make private
    public float MaxSpeed = 10f;
    public float DefaultMaxSpeed;
    public float JumpSpeed = 12f;
    public Transform JumpSensor;
    public float Radius;
    public LayerMask ConsideredGround;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool PlatformRelativeJump = false;

    MovingPlatform _movingPlatform;
    Rigidbody2D rbody2D;
    Animator _animator;
    bool _jumping;

    // Use this for initialization
    void Awake() {
        _animator = GetComponent<Animator>();
        rbody2D = GetComponent<Rigidbody2D>();
        DefaultMaxSpeed = MaxSpeed;
    }

    private void OnEnable() {
        MaxSpeed = DefaultMaxSpeed;
    }

    public void setSpeed(float coef) {
        MaxSpeed = coef;
    }

    public float getSpeed() {
        return MaxSpeed;
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
        //check if the player is grounded
        bool isGrounded = IsGrounded();

        //if the moving platform is not null, but the player is no longer on the ground,
        //get rid of the moving platform
        if (_movingPlatform != null && !isGrounded) {
            _movingPlatform = null;
        }

        //get the currect velocities 
        float xVelocity = rbody2D.velocity.x;
        float yVelocity = rbody2D.velocity.y;

        //if the player is on a moving platform and it is moving downwards
        if (isGrounded && rbody2D.velocity.y < 0 && _movingPlatform != null) {
            yVelocity = PlatformVelocity().y - 0.01f;

        }

        //for a more realistic jump
        if (yVelocity < 0 && _movingPlatform == null) {
            yVelocity += Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

            //this lets the character jump shorter
        } else if (yVelocity > 0 && !_jumping) {
            yVelocity += Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        //get the direction that the player should be moving
        xVelocity = Input.GetAxis("Horizontal") * MaxSpeed;

        //if the player is touching a moving platform, add the velocity of the moving
        //platform to the players velocity
        xVelocity += PlatformVelocity().x;


        //if the player has pressed jump and we are on the ground
        if (_jumping && isGrounded) {
            yVelocity = JumpSpeed;
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
