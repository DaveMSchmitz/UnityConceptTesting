using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : Buff {
    private PlayerAttack atk;
    private string name;
    private int e;
    private int d;
    private bool buffing;

    public AttackBuff(string name, int e, int d) {
        atk = GetComponent<PlayerAttack>();
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
        while (getDuration() > 0) {
            //health.changeHealth(getEffect());
            atk.modAttack(getEffect());
            yield return new WaitForSeconds(1);
            setDuration(getDuration() - 1);
        }
        atk.resetAttack();
    }
}
