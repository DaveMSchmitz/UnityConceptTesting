using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Rigidbody rigidbody = GetComponent<Rigidbody>();
	    camera.transform.position = new Vector3(rigidbody.position.x,rigidbody.position.y, -10.0f);
	}
}
