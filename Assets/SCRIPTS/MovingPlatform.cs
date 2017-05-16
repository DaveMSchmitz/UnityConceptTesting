using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform StartPos;
    public Transform EndPos;
    public GameObject Platform;
    public float MoveSpeed;

    [HideInInspector] public Rigidbody2D rbody;

    private Vector3 _target;

	// Use this for initialization
	void Start () {
        _target = StartPos.position;
        rbody = GetComponentInChildren<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 direction = (_target - Platform.transform.position).normalized;

        rbody.velocity = direction * MoveSpeed;


        if (Mathf.Abs(Platform.transform.position.x - _target.x) <= .25) {
            if(_target == StartPos.position) {
                _target = EndPos.position;
            } else {
                _target = StartPos.position;
            }
        }


    }
}
