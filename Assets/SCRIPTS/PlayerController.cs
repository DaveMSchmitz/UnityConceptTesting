using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Killable {
    public Vector3 RespawnTransform;
    private LevelManager levelManager;
    private HealthController health;
    private SpriteRenderer sprite;
    private Color color;

    // Use this for initialization
    void Start () {
		health = GetComponent<HealthController>();
        RespawnTransform = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
        health = GetComponent<HealthController>();
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn")
        {
            levelManager.Respawn();

        }

        //if the object hit is a checkpoint set the respawn transform to the transform of the object and set the checkpoint animation to set
        if (obj.tag == "Checkpoint")
        {
			Debug.Log ("CHECKPOINT");
            RespawnTransform = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, transform.position.z);
            obj.gameObject.GetComponent<Animator>().SetBool("check", true);
        }
		/*
		if (obj.tag == "Damage" && ambientDamageCoroutine == null)
		{
			Debug.Log ("AMBIENT DAMAGE");
			ambientDamageCoroutine = StartCoroutine ("aDmg");
		}
		if (obj.tag == "Enemy")
		{
			Debug.Log ("ENEMY DAMAGE");
			enemyDamageCoroutine = StartCoroutine ("eDmg");
		}
		*/
    }
	/*
	void OnTriggerExit2D(Collider2D obj)
	{
		if (obj.tag == "Damage") {
			
			Debug.Log ("STOP DAMAGE");
			StopCoroutine (ambientDamageCoroutine);
		}
		if (obj.tag == "Enemy")
		{
			Debug.Log ("STOP ENEMY DAMAGE");
			StopCoroutine (enemyDamageCoroutine);
		}
	}

	IEnumerator aDmg(){
		while (true) {
			health.changeHealth (-1);
			yield return new WaitForSeconds (2);
		}
	}

	IEnumerator eDmg(){
		while (true) {
			health.changeHealth (-1);
			yield return new WaitForSeconds (2);
		}
	}*/

    public override void killed() {
        levelManager.Respawn();
    }

    public override void healthChanged() {

        if (health.getIsAlive()) {
            StartCoroutine("blink");
        }

        levelManager.changeHealthText();
    }

    public IEnumerator blink() {

        

        sprite.color = new Color(color.r, color.g / 10, color.b / 10, 1);
        yield return new WaitForSeconds(.1f);
        sprite.color = color;
    }
}
