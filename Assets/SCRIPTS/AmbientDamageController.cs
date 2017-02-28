using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientDamageController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Debug.Log("Testing");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollision2DEnter(Collision2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.name.Equals("dt1"))
        {
            Destroy(col.gameObject);
        }
    }
}
