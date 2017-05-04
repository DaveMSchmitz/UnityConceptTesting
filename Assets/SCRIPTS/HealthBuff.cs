using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff {
    private HealthController health;
    private string name;
    private int e;
    private int d;

    public HealthBuff(string name, int e, int d) {
        health = GetComponent<HealthController>();
        this.name = name;
        this.e = e;
        this.d = d;
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
        health.changeHealth(getEffect());
        yield return new WaitForSeconds(1);
    }

}
