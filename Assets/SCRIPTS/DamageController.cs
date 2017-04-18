using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	private HealthController health;
	private Coroutine ambientDamageCoroutine;
	private Coroutine enemyDamageCoroutine;
	public LayerMask Damageable;

	public int aDamageCount;
	public int eDamageCount;

	public bool shouldStop;

	private float fireRate;
	private float nextFire;

	public bool isAttacking;

	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController>();
		fireRate = 1f;
		aDamageCount = 0;
		eDamageCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//Debug.Log (gameObject.name + " ATTACKING: Time.time: " + Time.time + " nextFire: " + nextFire);
			isAttacking = true;
		}
	}

	void OnTriggerStay2D(Collider2D obj)
	{
		if (isAttacking) {
			if (obj.tag == "Damageable") {
				//Debug.Log ("DAMAGEABLE!");
				//GetComponent (health).changeHealth (-1);
				obj.GetComponent<HealthController>().changeHealth(-1);
				Debug.Log("ATTACKING ENEMY: ENEMY HEALTH: " + obj.GetComponent<HealthController>().getCurrentHealth());
				isAttacking = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.tag == "Damage")
		{
			/*
			Debug.Log ("START AMBIENT DAMAGE");
			ambientDamageCoroutine = StartCoroutine ("aDmg");
			*/
			aDamageCount++;
			//Debug.Log ("START AMBIENT DAMAGE");
			
			ambientDamageCoroutine = StartCoroutine ("aDmg");
			
		}
		if (obj.tag == "Enemy")
		{
			eDamageCount++;
			//Debug.Log ("START ENEMY DAMAGE");
			
			enemyDamageCoroutine = StartCoroutine ("eDmg");
			
		}
		/*
		if (obj.tag == "Player")
		{
			Debug.Log ("AMBIENT DAMAGE");
			ambientDamageCoroutine = StartCoroutine ("aDmg");
		}*/
	}
	void OnTriggerExit2D(Collider2D obj)
	{
		if (obj.tag == "Damage") {
			/*
			Debug.Log ("STOP AMBIENT DAMAGE");
			StopCoroutine (ambientDamageCoroutine);
			*/
			aDamageCount--;
			//Debug.Log ("STOP ENEMY DAMAGE");
			if (aDamageCount == 0 && ambientDamageCoroutine != null) {
				//StopCoroutine (enemyDamageCoroutine);
				shouldStop = true;
				//enemyDamageCoroutine = null;
			}
		}
		if (obj.tag == "Enemy")
		{
			eDamageCount--;
			//Debug.Log ("STOP ENEMY DAMAGE");
			if (eDamageCount == 0 && enemyDamageCoroutine != null) {
				//StopCoroutine (enemyDamageCoroutine);
				shouldStop = true;
				//enemyDamageCoroutine = null;
			}
		}
		/*if (obj.tag == "Player")
		{
			Debug.Log ("STOP AMBIENT DAMAGE");
			StopCoroutine (ambientDamageCoroutine);
		}*/
	}

	IEnumerator aDmg(){
		/*
		while (true) {
			health.changeHealth (-1);
			invincible = true;
			yield return new WaitForSeconds (2);
			invincible = false;
		}
		*/
		while (!shouldStop) {
			health.changeHealth (-1);
			yield return new WaitForSeconds (2);
			

		}
		shouldStop = false;
	}

	IEnumerator eDmg(){
		while (!shouldStop) {
			health.changeHealth (-1);
			
			yield return new WaitForSeconds (2);

		}
		shouldStop = false;
	}
}
