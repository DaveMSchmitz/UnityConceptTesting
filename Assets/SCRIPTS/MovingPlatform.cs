using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform StartPos;
    public Transform EndPos;
    public GameObject Platform;
    public float MoveSpeed;

    [HideInInspector] public Rigidbody2D rigidbody;

    private Vector3 _target;

	// Use this for initialization
	void Start () {
        _target = StartPos.position;
	}
	
	// Update is called once per frame
	void Update () {

        Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, _target, MoveSpeed * Time.deltaTime);

        if (Platform.transform.position == EndPos.position) {
            _target = StartPos.position;
        }

        if (Platform.transform.position == StartPos.position) {
            _target = EndPos.position;
        }
    }
}
