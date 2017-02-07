using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFoo : MonoBehaviour
{

    public float movementSpeed;
    public float jumpSpeed;
    public Transform groundSensor;
    public float radius;
    public LayerMask consideredGround;
    public Vector3 respawnTransform;

    private Rigidbody2D playerRigidbody;
    private bool initJump;
    private float horizontal;
    private bool onGround;
    private Animator _playerAnimator;
    private bool canMove;


    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            //check to see if the player is pressing the arrow keys
            horizontal = Input.GetAxisRaw("Horizontal");

            //check to see if the character is on the ground
            CheckGround();

            //set the players orientation 
            if (horizontal != 0)
            {
                transform.localScale = new Vector3(horizontal, transform.localScale.y);
            }

            //move the player in the direction that the arrow key was pressed, if it wasn't then horizontal will be zero making the player stop
            playerRigidbody.velocity = new Vector2(horizontal * movementSpeed * Time.fixedDeltaTime, playerRigidbody.velocity.y);

            //checks if initJump is true. This code is actually already checked but because fixed update is called more than update it is possible to want to
            //jump and it wont jump so its a second check.
            if ((Input.GetButtonDown("Jump")) && onGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.fixedDeltaTime);


            }


        }

    }


    void LateUpdate()
    {
        _playerAnimator.SetBool("onGround", onGround);
        _playerAnimator.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));
    }

    private void CheckGround()
    {
        //check if the ground sensor is touching the ground
        onGround = Physics2D.OverlapCircle(groundSensor.position, radius, consideredGround); 
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Respawn")
        {
            transform.position = respawnTransform;

        }
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

}
