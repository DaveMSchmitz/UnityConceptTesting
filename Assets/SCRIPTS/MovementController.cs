using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpSpeed;
    public Transform GroundSensor;
    public float Radius;
    public LayerMask ConsideredGround;
    public float SlowingDown;

    private bool _onGround;
    private bool _triggerJump;
    private Rigidbody2D _playerRigidbody;
    private float _horizontal;
    private float _movement;

    // Use this for initialization
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the direction the player wants to move
        _horizontal = Input.GetAxisRaw("Horizontal");

        //because direction is based on what way the player is moving, don't set orientation if player is not moving
        if (_horizontal != 0) {
            //set the players orientation 
            transform.localScale = new Vector3(Mathf.Sign(_horizontal), transform.localScale.y);
            _movement = _horizontal;
        } else {
            if (Mathf.Abs(_movement) < 0.1) {
                _movement = 0;
            }
            _movement = Mathf.Lerp(_movement, 0, SlowingDown * Time.deltaTime);

        }

        //check to see if the player is on the ground
        _onGround = CheckGround();

        //if the player presses the jump button and is on the ground for this frame set the trigger to true
        //so that when the next fixedUpdate comes around, jump
        if (Input.GetButtonDown("Jump") && _onGround) {
            _triggerJump = true;
        }
    }

    void FixedUpdate() {

        //if the jump is triggered
        if (_triggerJump) {

            //add velocity to the player so that they will jump, and reset the trigger
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, JumpSpeed * Time.fixedDeltaTime);
            _triggerJump = false;
        }

        //move the player bassed on what the _horizontal was
        _playerRigidbody.velocity = new Vector2(_movement * MovementSpeed * Time.fixedDeltaTime, _playerRigidbody.velocity.y);

    }

    public bool CheckGround() {
        //check if the ground sensor is touching the ground
        return Physics2D.OverlapCircle(GroundSensor.position, Radius, ConsideredGround);
    }
}
