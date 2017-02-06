using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject playerCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
	    playerCamera.transform.position = new Vector3(rigidbody2D.position.x,rigidbody2D.position.y, -10.0f);
	}
}
