using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : Buff {
    private PlayerAttack atk;
    private string name;
    private int e;
    private int d;

    public AttackBuff(string name, int e, int d) {
        atk = GetComponent<PlayerAttack>();
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
        yield return new WaitForSeconds(1);
    }
}
