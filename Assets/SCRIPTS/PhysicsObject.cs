using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float gravityModifier = 3f;
    

    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    protected Rigidbody2D _rigidbody;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected bool isGrounded;
    protected Vector2 groundNormal;

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;
    private const float minGroundNormalY = .999999f;

    void OnEnable() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start() {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update() {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity() {

    }

    void FixedUpdate() {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        isGrounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        //move horizontally
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);

        //move vertically
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);

        
    }

    void Movement(Vector2 move, bool yMovement) {

        //set this to the amount that you want to move
        float distance = move.magnitude;

        //don't check for collisions if your not moving
        if(distance > minMoveDistance) {

            //count is the number of things that you hit when you cast. Move is the direction, the contactFilter
            //is the things that you want to collide with (set in the settings in unity), hitbuffer will return all
            //of the things that we hit, and the distance that we are going to cast is the distance we are trying to move
            // plus just a little bit so that we don't get stuck inside of things
            int count =_rigidbody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            //clear the list to git rid of the old data from last frame
            hitBufferList.Clear();

            //this loop grabs all of the hits from the hitBuffer and puts them into the list
            for (int i = 0; i < count; ++i) {
                hitBufferList.Add(hitBuffer[i]);
                

            }

            //this loop runs through the list we just made
            for (int i = 0; i < hitBufferList.Count; ++i) {

                //this is the current normal that our object has hit
                Vector2 currentNormal = hitBufferList[i].normal;

                //if the current y is within the range that we consider ground, set the object to grounded
                if (currentNormal.y > minGroundNormalY) {
                    isGrounded = true;

                    //if you are calculating y movement, set the groundNormal to the current normal and 
                    //disregard the x component because this conditional only handles y movement
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }

                }

                //this will allow us to maintain velocity if we hit something
                float projection = Vector2.Dot(velocity, currentNormal);

                if (projection < 0) {
                    velocity = velocity - projection * currentNormal;
                }

                //this will protect us if we are inside something move us outside of it
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }
        _rigidbody.position = _rigidbody.position + move.normalized * distance;
        

    }
}
