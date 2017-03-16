using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTemp : MonoBehaviour {

	public float Damage;
	public float CoolDown;

	private float nextFire;
	private List<GameObject> enemies;


	// Use this for initialization
	void Start () {
		nextFire = 0;
		enemies = new List<GameObject>();
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
		    foreach (GameObject enemy in enemies)
		    {
		        if (enemy != null)
		        {
                    EnemyController ec = enemy.GetComponent<EnemyController>();
                    ec.health.changeHealth(-5);
                    Debug.Log(ec.health.getCurrentHealth());
                }

			}


		}

	    enemies.RemoveAll(item => item == null);
	}
		
	void OnTriggerEnter2D(Collider2D col){
		//if you touch an enemy, keep track of that enemy
		if (col.tag == "Enemy") {
			enemies.Add(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		//when that enemy is no longer tounching you, get rid of that reference
		if (col.tag == "Enemy")
		{
		    enemies.Remove(col.gameObject);
		}
	}
}
