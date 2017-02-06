using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerFoo : MonoBehaviour
{

    public GameObject focus;
    public float cameraBuffer;
    public float smoothness;

    private Vector3 focusPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	    float x = focus.transform.position.x + Mathf.Sign(focus.transform.localScale.x) * cameraBuffer;
	    float y = transform.position.y;
	    float z = transform.position.z;

        focusPosition = new Vector3(x,y,z);
	    transform.position = Vector3.Lerp(transform.position, focusPosition, smoothness * Time.deltaTime);

	}
}
