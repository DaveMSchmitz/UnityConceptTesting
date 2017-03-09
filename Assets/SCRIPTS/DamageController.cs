﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    private PlayerController player;
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
        //Debug.Log("Dealing damage to player");
        if (obj.CompareTag("Player"))
        {

            dmgCoroutine = StartCoroutine(dmg());

        } else if (player.isAttacking && obj.CompareTag("Enemy") && this.CompareTag("Weapon")) {
            
            

        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        //if (obj.CompareTag("Damage"))
        if (obj.CompareTag("Player") && this.CompareTag("Enemy"))
        {
            Debug.Log("Stop dealing damage to player");
            StopCoroutine(dmgCoroutine);
        } else if (player.isAttacking && obj.CompareTag("Enemy") && this.CompareTag("Weapon")) {
            Debug.Log("Stop dealing damage to enemy");
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
                player.health.setHealth(player.health.getMaxHealth());
                player.transform.position = player.RespawnTransform;
                
            }
            yield return new WaitForSeconds(2);
        }
    }
}
