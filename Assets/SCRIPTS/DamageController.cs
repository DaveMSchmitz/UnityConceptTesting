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

	public bool invincible;
	public bool shouldStop;

	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController>();
		aDamageCount = 0;
		eDamageCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
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
			Debug.Log ("START AMBIENT DAMAGE");
			if (!invincible) {
				ambientDamageCoroutine = StartCoroutine ("aDmg");
			}
		}
		if (obj.tag == "Enemy")
		{
			eDamageCount++;
			Debug.Log ("START ENEMY DAMAGE");
			if (!invincible) {
				enemyDamageCoroutine = StartCoroutine ("eDmg");
			}
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
			Debug.Log ("STOP ENEMY DAMAGE");
			if (aDamageCount == 0 && ambientDamageCoroutine != null) {
				//StopCoroutine (enemyDamageCoroutine);
				shouldStop = true;
				//enemyDamageCoroutine = null;
			}
		}
		if (obj.tag == "Enemy")
		{
			eDamageCount--;
			Debug.Log ("STOP ENEMY DAMAGE");
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
			invincible = true;
			yield return new WaitForSeconds (2);
			invincible = false;

		}
		shouldStop = false;
	}

	IEnumerator eDmg(){
		while (!shouldStop) {
			health.changeHealth (-1);
			invincible = true;
			yield return new WaitForSeconds (2);
			invincible = false;

		}
		shouldStop = false;
	}
}
