using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientDamageController : MonoBehaviour {
    public string name;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == name)
        {
            Destroy(col.gameObject);
        }
    }
}
