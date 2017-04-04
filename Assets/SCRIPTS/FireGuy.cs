using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGuy : MonoBehaviour {

    [SerializeField]
    private Transform FloatingWayPoint;

    [SerializeField]
    private float MoveSpeed;

    private new Rigidbody2D rigidbody;
    private Vector3 target;


	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        target = FloatingWayPoint.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Mathf.Abs(target.y - transform.position.y) < .1) {
            target = new Vector3(Random.Range(FloatingWayPoint.position.x - 2, FloatingWayPoint.position.x + 2), Random.Range(FloatingWayPoint.position.y - 2, FloatingWayPoint.position.y + 2), transform.position.z);
        } else {
            rigidbody.MovePosition(transform.position + (target - transform.position).normalized * (MoveSpeed  * Time.deltaTime));
        }

        
	}
}
