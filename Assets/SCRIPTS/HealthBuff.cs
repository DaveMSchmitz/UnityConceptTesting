using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff {
    private HealthController health;
    private string name;
    private int e;
    private int d;
    private Animator anim;
    public bool buffing;

    //public HealthBuff(string name, int e, int d, Animator anim) {
    public HealthBuff(string name, int e, int d) {
        health = GetComponent<HealthController>();
        this.name = name;
        this.e = e;
        this.d = d;
        this.anim = anim;
        buffing = false;
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
        while (getDuration() > 0) {
            setBuffing(true);
            health.changeHealth(getEffect());
            yield return new WaitForSeconds(1);
            setDuration(getDuration() - 1);

        }
        setBuffing(false);
    }
}
