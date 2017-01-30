using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{


	    Rigidbody rigidbody = GetComponent<Rigidbody>();
	    float velocity = rigidbody.velocity.y;

	    float horizontal = Input.GetAxis("Horizontal");

	    if (Input.GetButtonDown("Jump") && velocity <= 0.0001 && velocity >= -0.0001)
	    {
	        rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed); 
	    }
        
        Vector2 offset = new Vector2(horizontal, 0.0f) * speed * Time.deltaTime;
        transform.Translate(offset);


	}
}
