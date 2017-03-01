using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    public bool takeDamage;
    public string tag;
    // Use this for initialization
    void Start () {
        Debug.Log("Damage testing");
        var player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D obj)
    {
        takeDamage = true;
        //This damages the player
        //if(obj.CompareTag("Damage"))
        if(obj.CompareTag(tag))
        {
            while (takeDamage){
                
            }
            Debug.Log("Dealing damage to player");
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        //if (obj.CompareTag("Damage"))
        if(obj.CompareTag(tag))
        {
            takeDamage = false;
            Debug.Log("Stop dealing damage to player");
        }

    }
}
