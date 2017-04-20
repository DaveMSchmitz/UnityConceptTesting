using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Killable {

    private HealthController health;
    private Slider healthBar;

    void Start(){
        health = GetComponent<HealthController>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.value = health.getCurrentHealth();

	}

    public override void healthChanged() {
        healthBar.value = (float)health.getCurrentHealth() /health.getMaxHealth();
    }

    public override void killed() {
        health.setHealth(health.getMaxHealth());
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn")
        {
            gameObject.SetActive(false);

        }

    }
}
