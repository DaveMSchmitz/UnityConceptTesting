﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector3 RespawnTransform;
    // Use this for initialization
    void Start () {
        RespawnTransform = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = RespawnTransform;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn")
        {
            transform.position = RespawnTransform;

        }

        //if the object hit is a checkpoint set the respawn transform to the transform of the object and set the checkpoint animation to set
        if (obj.tag == "Checkpoint")
        {
            RespawnTransform = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, transform.position.z);
            obj.gameObject.GetComponent<Animator>().SetBool("check", true);
        }
    }
}
