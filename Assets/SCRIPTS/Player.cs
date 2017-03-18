using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float MovementSpeed = 10;
    public float JumpHeight = 4;
    public float TimeToJumpHeight = .4f;
    public float Acceleration = .1f;
    public Vector3 RespawnTransform;


    private float _smoothing;
    private Vector3 _velocity;
    private Controller2D _controller;
    private float _jumpSpeed;
    private float _gravity;
    private Animator animator;
    // Use this for initialization
    void Start ()
	{
	    _controller = GetComponent<Controller2D>();
	    _gravity = -(2 * JumpHeight) / Mathf.Pow(TimeToJumpHeight, 2);
	    _jumpSpeed = Mathf.Abs(_gravity) * TimeToJumpHeight;
	    animator = GetComponent<Animator>();
        RespawnTransform = transform.position;

    }

    // Update is called once per frame
    void Update ()
	{

	    if (_controller.Collision.above || _controller.Collision.below)
	    {
	        _velocity.y = 0;
	    }

	    if (_controller.Collision.right || _controller.Collision.left)
	    {
	        _velocity.x = 0;
	    }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

	    if (Input.GetButtonDown("Jump") && _controller.Collision.below)
	    {
	        _velocity.y = _jumpSpeed;
	    }
	    float targetVelocity = input.x * MovementSpeed;

	    _velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocity, ref _smoothing, Acceleration);

	    _velocity.y +=_gravity * Time.deltaTime;
	    _controller.Move(_velocity*Time.deltaTime);

	    if (_velocity.x != 0)
	    {
	        transform.localScale = new Vector3(Mathf.Sign(_velocity.x), transform.localScale.y);
        }
	}

    void LateUpdate()
    {
        animator.SetBool("onGround", _controller.Collision.below);
        animator.SetFloat("Speed", Mathf.Abs(_velocity.x));
    }

    void OnTriggerEnter2D(Collider2D obj) {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn") {
            transform.position = RespawnTransform;

        }

        //if the object hit is a checkpoint set the respawn transform to the transform of the object and set the checkpoint animation to set
        if (obj.tag == "Checkpoint") {
            RespawnTransform = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, transform.position.z);
            obj.gameObject.GetComponent<Animator>().SetBool("check", true);
        }
    }
}
