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
		enemies = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + CoolDown;
			Debug.Log("Attack");



		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Enemy") {
			enemies.Add(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Enemy") {
			enemies.Remove (col.gameObject);
		}
	}
}
