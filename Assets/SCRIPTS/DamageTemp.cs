﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTemp : MonoBehaviour {

	public float Damage;
	public float CoolDown;

	private float nextFire;
	private GameObject enemies;


	// Use this for initialization
	void Start () {
		nextFire = 0;
		enemies = null;
	}
	
	// Update is called once per frame
	void Update () {

		//if the player presses the fire button and the time is bigger than the cooldown time
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			//set the next oppurtinuty they can fire
			nextFire = Time.time + CoolDown;
			Debug.Log("Attack");

			//if the weapon is touching an enemy gut it
			if (enemies != null) {
				EnemyController ec = enemies.GetComponent<EnemyController> ();
				ec.health.changeHealth (-5);
				Debug.Log (ec.health.getCurrentHealth());
			}


		}
	}
		
	void OnTriggerEnter2D(Collider2D col){
		//if you touch an enemy, keep track of that enemy
		if (col.tag == "Enemy") {
			enemies = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		//when that enemy is no longer tounching you, get rid of that reference
		if (col.tag == "Enemy") {
			enemies = null;
		}
	}
}
