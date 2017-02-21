using UnityEngine;


public class PlayerControllerFoo : MonoBehaviour {

    public float MovementSpeed;
    public float JumpSpeed;
    public Transform GroundSensor;
    public float Radius;
    public LayerMask ConsideredGround;
    public Vector3 RespawnTransform;

    private Rigidbody2D _playerRigidbody;
    private float _horizontal;
    private bool _onGround;
    private Animator _playerAnimator;


    private bool _triggerJump;
    private bool _triggerMove;

    // Use this for initialization
    void Start() {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        //get the direction the player wants to move
        _horizontal = Input.GetAxisRaw("Horizontal");

        //because direction is based on what way the player is moving, don't set orientation if player is not moving
        if (_horizontal != 0) {
            //set the players orientation 
            transform.localScale = new Vector3(_horizontal, transform.localScale.y);
        }

        //check to see if the player is on the ground
        CheckGround();

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
        _playerRigidbody.velocity = new Vector2(_horizontal * MovementSpeed * Time.fixedDeltaTime, _playerRigidbody.velocity.y);

    }

    void LateUpdate() {
        //after all of the physics, set the animation of the player
        _playerAnimator.SetBool("onGround", _onGround);
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_playerRigidbody.velocity.x));
    }

    private void CheckGround() {
        //check if the ground sensor is touching the ground
        _onGround = Physics2D.OverlapCircle(GroundSensor.position, Radius, ConsideredGround);
    }

    void OnTriggerEnter2D(Collider2D obj) {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if(obj.tag == "Respawn") {
            transform.position = RespawnTransform;

        }

        //if the object hit is a checkpoint set the respawn transform to the transform of the object and set the checkpoint animation to set
        if(obj.tag == "Checkpoint") {
            RespawnTransform = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, transform.position.z);
            obj.gameObject.GetComponent<Animator>().SetBool("check", true);
        }
    }
}
