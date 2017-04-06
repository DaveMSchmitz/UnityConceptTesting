using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	private HealthController health;
	private Coroutine ambientDamageCoroutine;
	private Coroutine enemyDamageCoroutine;
	public LayerMask Damageable;
	public int count;
	public bool invincible;
	public bool shouldStop;

	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController>();
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		
		if (obj.tag == "Damage" && ambientDamageCoroutine == null)
		{
			Debug.Log ("AMBIENT DAMAGE");
			ambientDamageCoroutine = StartCoroutine ("aDmg");
		}
		if (obj.tag == "Enemy")
		{
			count++;
			Debug.Log ("ENEMY DAMAGE");
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

			Debug.Log ("STOP AMBIENT DAMAGE");
			StopCoroutine (ambientDamageCoroutine);
		}
		if (obj.tag == "Enemy")
		{
			count--;
			Debug.Log ("STOP ENEMY DAMAGE");
			if (count == 0 && enemyDamageCoroutine != null) {
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
		while (true) {
			health.changeHealth (-1);
			invincible = true;
			yield return new WaitForSeconds (2);
			invincible = false;
		}
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
