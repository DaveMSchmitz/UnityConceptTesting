using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public HealthController health;
    public bool isAttacking;

    public Vector3 RespawnTransform;

    private Animator _enemyAnimator;
    private Rigidbody2D _enemyRigidbody;

    private float nextFire;
    public float fireRate;

    private MovementController _movement;



    // Use this for initialization
    void Start()
    {
        health = new HealthController(10, 10);
        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!health.getIsAlive()) {
			Destroy (gameObject);
		}
    }



    void LateUpdate()
    {
        //after all of the physics, set the animation of the player
        _enemyAnimator.SetBool("onGround", _movement.CheckGround());
        _enemyAnimator.SetFloat("Speed", Mathf.Abs(_enemyRigidbody.velocity.x));
    }

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Respawn") {
			Destroy (gameObject);
		}
	}
		
}
