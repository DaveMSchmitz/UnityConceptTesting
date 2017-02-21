using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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

    private bool triggerJump;
    private bool triggerMove;

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
#if UNITY_ANDROID
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                horizontal = 1;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            {
                horizontal = -1;
            }
            else if (CrossPlatformInputManager.GetButtonUp("Fire1") || CrossPlatformInputManager.GetButtonUp("Fire2"))
            {
                horizontal = 0;
            }
#else
            horizontal = Input.GetAxisRaw("Horizontal");
#endif
            


            if (horizontal != 0)
            {
                //set the players orientation 
                transform.localScale = new Vector3(horizontal, transform.localScale.y);
            }

            //move the player in the direction that the arrow key was pressed, if it wasn't then horizontal will be zero making the player stop
           
            


            //check to see if the character is on the ground

            //checks if initJump is true. This code is actually already checked but because fixed update is called more than update it is possible to want to
            //jump and it wont jump so its a second check.
            CheckGround();

            if (Input.GetButtonDown("Jump") && onGround)
            {
                triggerJump = true;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(onGround.ToString());
            }


        }

    }

    void FixedUpdate()
    {
       

        if (triggerJump)
        {
            Debug.Log(onGround.ToString());
        }

        if (triggerJump)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.fixedDeltaTime);
            triggerJump = false;
        }

        playerRigidbody.velocity = new Vector2(horizontal * movementSpeed * Time.fixedDeltaTime, playerRigidbody.velocity.y);

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

        if (obj.tag == "Checkpoint")
        {
            respawnTransform = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, transform.position.z);
            obj.gameObject.GetComponent<Animator>().SetBool("check", true);
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
