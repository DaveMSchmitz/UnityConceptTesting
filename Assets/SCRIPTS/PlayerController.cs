using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Killable {
    public Vector3 RespawnTransform;
    private LevelManager levelManager;
	//public HealthController health = new HealthController (10, 10);
	private HealthController health;
	private Coroutine dmgCoroutine;

    
    // Use this for initialization
    void Start () {
		health = GetComponent<HealthController>();
        RespawnTransform = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
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

		if (obj.tag == "Damage")
		{
			Debug.Log ("DAMAGE");
			dmgCoroutine = StartCoroutine ("dmg");
		}
    }
	void OnTriggerExit2D(Collider2D obj)
	{
		if (obj.tag == "Damage") {
			
			Debug.Log ("STOP DAMAGE");
			StopCoroutine (dmgCoroutine);
		}
	}

	IEnumerator dmg(){
		while (true) {
			health.changeHealth (-1);
			yield return new WaitForSeconds (2);
		}
	}

    public override void killed() {
        levelManager.Respawn();
    }

    public override void healthChanged() {
        levelManager.changeHealthText();
    }
}
