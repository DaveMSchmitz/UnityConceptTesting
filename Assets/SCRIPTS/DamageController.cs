﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    private PlayerController player;
    private bool takeDamage;
    private Coroutine dmgCoroutine;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("/Player").GetComponent<PlayerController>();
        Debug.Log("Damage testing");
        //var player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D obj)
    {
        //This damages the player
        //if(obj.CompareTag("Damage"))
        if(obj.CompareTag("Player"))
        {
            //takeDamage = true;
            dmgCoroutine = StartCoroutine(dmg());
            //Debug.Log("Dealing damage to player");
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        //if (obj.CompareTag("Damage"))
        if(obj.CompareTag("Player"))
        {
            takeDamage = false;
            Debug.Log("Stop dealing damage to player");
            StopCoroutine(dmgCoroutine);
        }

    }

    IEnumerator dmg()
    {
        while (true)
        {
            Debug.Log("Health: " + player.health.getCurrentHealth());
            player.health.changeHealth(-1);
            if (!player.health.getIsAlive())
            {
                player.transform.position = player.RespawnTransform;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
