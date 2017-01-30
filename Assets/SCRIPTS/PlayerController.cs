using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
    private bool onFloor = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
            //Handles the Left and right movement
	    float horizontal = Input.GetAxis("Horizontal");
        
        Vector2 offset = new Vector2(horizontal, 0.0f) * speed * Time.deltaTime;
        transform.Translate(offset);

            //Handles the jump
	    if (Input.GetButtonDown("Jump") && onFloor)
	    {
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
	    }
        

	}

    //Checks to see if the object is colliding with the ground
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Floor"))
        {
            if (ReturnDirection(obj.gameObject, this.gameObject) == HitDirection.Top)
            {
                onFloor = true;
            }
        }
    }

    //check to see if the object has stopped colliding with the ground
    void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }

    private  enum HitDirection { None, Top, Bottom, Left, Right}

    private HitDirection ReturnDirection(GameObject player, GameObject objectCollided)
    {
        
        HitDirection hitDirection = HitDirection.None;

        Vector3 direction = (player.transform.position - objectCollided.transform.position).normalized;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(objectCollided.transform.position, direction);


        if (raycastHit2D)
        {
            if (raycastHit2D.collider != null)
            {
                Vector3 normal = raycastHit2D.normal;
                normal = raycastHit2D.transform.TransformDirection(normal);

                if(normal == raycastHit2D.transform.up) {hitDirection = HitDirection.Top; }
                if (normal == -raycastHit2D.transform.up) {hitDirection = HitDirection.Bottom; }
                if (normal == raycastHit2D.transform.right) {hitDirection = HitDirection.Right; }
                if (normal == -raycastHit2D.transform.right) {hitDirection = HitDirection.Left; }

            }
        }




        return hitDirection;
    }

}
