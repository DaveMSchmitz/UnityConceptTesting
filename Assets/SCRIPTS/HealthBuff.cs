using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff {
    private HealthController health;
    private int e;
    private int d;
    public Animator anim;
    public bool buffing;

    private void OnEnable() {
        health = GetComponent<HealthController>();
        e = 0;
        d = 0;
        buffing = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "fire_buff" && !getBuffing()) {
            Debug.Log("Fire Buff");
            setEffect(-1);
            setDuration(5);
            StartCoroutine("buff");
        }
    }

    public override void setBuffing(bool buffing) {
        this.buffing = buffing;
    }

    public override bool getBuffing() {
        return buffing;
    }

    public override void setEffect(int e) {
        this.e = e;
        Debug.Log("Effect: " + this.e);
    }

    public override void setDuration(int d) {
        this.d = d;
        Debug.Log("Duration");
    }

    public override int getEffect() {
        return e;
    }

    public override int getDuration() {
        return d;
    }

    public override IEnumerator buff() {
        Debug.Log("Buffing!");
        while (getDuration() > 0) {
            setBuffing(true);
            health.changeHealth(getEffect());
            yield return new WaitForSeconds(1);
            setDuration(getDuration() - 1);

        }
        Debug.Log("Not Buffing!");
        setBuffing(false);
    }
}
