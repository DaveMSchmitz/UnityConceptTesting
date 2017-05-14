﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBuff : Buff {
    private PlayerMovementController movement;
    private string name;
    private int e;
    private int d;
    private bool buffing;

    public MovementBuff(string name, int e, int d) {
        movement = GetComponent<PlayerMovementController>();
        this.name = name;
        this.e = e;
        this.d = d;
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
        yield return new WaitForSeconds(1);
    }
}
