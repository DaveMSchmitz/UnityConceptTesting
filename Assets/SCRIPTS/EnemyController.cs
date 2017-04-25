using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Killable {

    private HealthController health;
    private Slider healthBar;
    private SpriteRenderer sprite;
    private Color color;

    void Start() {
        health = GetComponent<HealthController>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.value = health.getCurrentHealth();
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    void OnDisable() {
        sprite.color = color;
    }

    public override void healthChanged() {
        healthBar.value = (float)health.getCurrentHealth() / health.getMaxHealth();

        if (health.getIsAlive()) {
            StartCoroutine("blink");
        }
    }

    public override void killed() {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D obj) {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn") {
            gameObject.SetActive(false);

        }

    }

    public IEnumerator blink() {

        sprite.color = new Color(color.r, color.g/10, color.b/10, 1);
        yield return new WaitForSeconds(.1f);
        sprite.color = color;
    }
}
