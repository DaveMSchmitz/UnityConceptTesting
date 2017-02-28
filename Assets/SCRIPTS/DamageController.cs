using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientDamageController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Debug.Log("Damage testing");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D obj)
    {

        
        if(obj.name == "dt1")
        {
            Debug.Log("Destroying dt1");
            Destroy(obj.gameObject);
        }
    }
}
