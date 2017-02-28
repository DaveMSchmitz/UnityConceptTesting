using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float jumpSpeed;
    public float wallJumpPower;
    public float maxSpeed;
    public float groundAcceleration;
    public float airAcceleration;
    public float deceleration;
    public float rayLength;
    public float sideRayLength;

    public float x;
    public float y;
 
    private Rigidbody2D playerRigidbody;
   
    private float horizontal;
    private bool initJump = false;
    private float acceleration;
    

	// Use this for initialization
	void Start ()
	{
        //set the default rigidbody
	    playerRigidbody = GetComponent<Rigidbody2D>();
        //because the player drops in make it easy and set it to air accel
	    acceleration = airAcceleration;

	}
	
	// Update is called once per frame, button presses should be captured in this
	void Update ()
	{
        //capture the button press and set a variable to trigger the physics system to move left or right on next fixed update
        horizontal = Input.GetAxisRaw("Horizontal");
       


        //capture the button press and set a variable to trigger the physics system to jump on next fixed update
        if (Input.GetButtonDown("Jump"))
	    {
	        initJump = true;
	    }

        //
        Debug.DrawRay(transform.position, new Vector2(x, -y).normalized * sideRayLength, Color.blue);
        Debug.DrawRay(transform.position, new Vector2(-x, -y).normalized * sideRayLength, Color.blue);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.cyan);
    }

    void FixedUpdate()
    {
       
        //if the jump has been triggered jump, if not make sure that the input wasnt missed.  Ususally you do not want inputs in the class
        if ((initJump))
        {
            
            if (CheckGround())
            {
                Jump();
            }
            else if (CheckWall())
                WallJump();
        }
    
        Move();
        initJump = false;

    }

    private void Jump()
    {
       playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed * Time.fixedDeltaTime * 10);
        initJump = false;
        acceleration = airAcceleration;



    }

    private void WallJump()
    {

        playerRigidbody.velocity = new Vector2(wallJumpPower * Time.fixedDeltaTime * 13 * -horizontal, wallJumpPower * Time.fixedDeltaTime * 5);
        initJump = false;
    }

    private void Move()
    {
       
        float tempDeceleration = 0;
        if (horizontal == 0 && (int)playerRigidbody.velocity.x != 0)
        {
            tempDeceleration = deceleration * -Mathf.Sign(playerRigidbody.velocity.x);
              
            
        }

        float temp = (horizontal * acceleration * Time.fixedDeltaTime * 10) + (playerRigidbody.velocity.x + tempDeceleration);
        if (horizontal == 1)
        {
            temp = Mathf.Clamp(temp, int.MinValue, maxSpeed);
           
        }else if (horizontal == -1)
        {
           temp = Mathf.Clamp(temp, -maxSpeed, int.MaxValue);
            
        }


        playerRigidbody.velocity = new Vector2(temp, playerRigidbody.velocity.y);
    }


    private bool CheckGround()
    {
        bool result = false;
        
        
        if (Physics2D.Raycast(transform.position,Vector2.down, rayLength))
        {
            result = true;
        }else if (Physics2D.Raycast(transform.position, new Vector2(x , -y), sideRayLength))
        {
            
            result = true;
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(-x , -y), sideRayLength))
        {
            result = true;
        }
        


        return result;
    }

    private bool CheckWall()
    {
        bool result = false;
       


        if (Physics2D.Raycast(transform.position, Vector2.right, rayLength))
        {
            result = true;
        }
        else if (Physics2D.Raycast(transform.position,Vector2.left, rayLength))
        {
            result = true;
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(x, -y), sideRayLength))
        {
            result = true;
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(-x, -y), sideRayLength))
        {
            result = true;
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(-x, y), sideRayLength))
        {
            result = true;
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(-x, -y), sideRayLength))
        {
            result = true;
        }




        return result;
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Floor"))
        {
            acceleration = groundAcceleration;
        }
    }
}
