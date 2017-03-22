using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Vector3 RespawnTransform;
    // Use this for initialization
    void Start(){
        RespawnTransform = transform.position;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D obj)
    {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn")
        {
            transform.position = RespawnTransform;

        }

    }
}
