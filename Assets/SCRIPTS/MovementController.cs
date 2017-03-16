using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour {
    public float MovementSpeed;
    public float JumpSpeed;
    public float Acceleration;
    public float Decceleration;
    public float AirAcceleration;
    public float AirDecceleration;
    public Transform GroundSensor;
    public float Radius;
    public LayerMask ConsideredGround;

    private bool _onGround;
    private bool _triggerJump;
    private Rigidbody2D _playerRigidbody;
    private float _horizontal;
    private float _movement;
    private float _accelerationType;
    private float _deccelerationType;

	public abstract float getMovement ();
	public abstract bool getJump ();
	public abstract void setUp ();

    // Use this for initialization
    void Start() {
        _playerRigidbody = GetComponent<Rigidbody2D>();
		setUp ();
    }

    // Update is called once per frame
    void Update() {
        //get the direction the player wants to move
		_horizontal = getMovement();

        //because direction is based on what way the player is moving, don't set orientation if player is not moving
        if (_horizontal != 0) {
            //set the players orientation 
            transform.localScale = new Vector3(Mathf.Sign(_horizontal), transform.localScale.y);
        }

        CalcMove();
        //check to see if the player is on the ground
        _onGround = CheckGround();

        //if the player presses the jump button and is on the ground for this frame set the trigger to true
        //so that when the next fixedUpdate comes around, jump
		if (getJump() && _onGround) {
            _triggerJump = true;
        }

    }

    void FixedUpdate() {

        //if the jump is triggered
        if (_triggerJump) {

            //add velocity to the player so that they will jump, and reset the trigger
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, JumpSpeed);
            _triggerJump = false;
        }

        //move the player bassed on what the _horizontal was
        _playerRigidbody.velocity = new Vector2(_movement, _playerRigidbody.velocity.y);

    }

    public bool CheckGround() {
        //check if the ground sensor is touching the ground
        bool result = Physics2D.OverlapCircle(GroundSensor.position, Radius, ConsideredGround);

        if (result)
        {
            _accelerationType = Acceleration;
            _deccelerationType = Decceleration;
        }
        else
        {
            _accelerationType = AirAcceleration;
            _deccelerationType = AirDecceleration;
        }

        return result;
    }

    //calculate the move 
    private void CalcMove() {

        //if the player isn't standing still
        if (_horizontal != 0) {

            //accelerate the player unless..
            Accelerate();

            //the player has switched the direction they are moving, then deccelerate them before accelerating them
            if (Mathf.Sign(_movement) != Mathf.Sign(transform.localScale.x)) {
                Deccelerate();
            }


            //if the player has stopped pressing the move button make them deccelerate
        } else {
            Deccelerate();
        }


    }

    //accelerate the player
    private void Accelerate() {
        _movement = _playerRigidbody.velocity.x + (_accelerationType * Time.fixedDeltaTime * _horizontal);
        _movement = Mathf.Clamp(_movement, -MovementSpeed, MovementSpeed);
    }

    //decelerate the player
    private void Deccelerate() {

        _movement = _playerRigidbody.velocity.x + (_deccelerationType * Time.fixedDeltaTime * -transform.localScale.x);

        if (Mathf.Sign(_movement) != Mathf.Sign(transform.localScale.x)) {
            _movement = 0;
        }

    }
		
}
