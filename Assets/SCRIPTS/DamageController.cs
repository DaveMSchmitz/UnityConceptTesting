using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Debug.Log("Damage testing");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D obj)
    {

        //This damages the player
        if(obj.CompareTag("Damage"))
        {
            Debug.Log("Dealing damage to player");


            //Debug.Log("Destroying dt1");
            //Destroy(obj.gameObject);
        }
    }
}
