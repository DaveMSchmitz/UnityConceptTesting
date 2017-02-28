using UnityEngine;
using UnityEngine.Assertions.Comparers;


public class PlayerControllerFoo : MonoBehaviour
{

    public Health health;

    public Vector3 RespawnTransform;
 
    private Animator _playerAnimator;
    private Rigidbody2D _playerRigidbody;

    private MovementController movement;



    // Use this for initialization
    void Start() {
        health = new Health(25);
        _playerAnimator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update() {

        
    }



    void LateUpdate() {
        //after all of the physics, set the animation of the player
        _playerAnimator.SetBool("onGround", movement.CheckGround());
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_playerRigidbody.velocity.x));
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
