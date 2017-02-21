using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{

    public float killZoneY;
   
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
	{
	    
		transform.position = new Vector3(Camera.main.transform.position.x, killZoneY, transform.position.z);
	}
}
