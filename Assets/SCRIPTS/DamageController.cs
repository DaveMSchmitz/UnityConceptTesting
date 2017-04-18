using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    [SerializeField]
    private LayerMask Damageable;

    [SerializeField]
    private int AttackStrength = 1;

    [SerializeField]
    private int CoolDown = 1;

    private bool canAttack = true;


    void OnTriggerStay2D(Collider2D obj) {

        if (canAttack && Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            obj.gameObject.GetComponent<HealthController>().changeHealth(-AttackStrength);
            StartCoroutine("Dmg");
        }

    }

    void OnTriggerEnter2D(Collider2D obj) {

        if (canAttack && Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            obj.gameObject.GetComponent<HealthController>().changeHealth(-AttackStrength);
            StartCoroutine("Dmg");
        }

    }

    public IEnumerator Dmg() {
        canAttack = false;
        yield return new WaitForSeconds(CoolDown);
        canAttack = true;
    }


}
