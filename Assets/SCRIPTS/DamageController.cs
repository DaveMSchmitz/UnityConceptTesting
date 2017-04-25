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

    [SerializeField]
    private Animator Weapon;

    private bool canAttack = true;

    void OnEnable() {
        canAttack = true;
    }

    void OnTriggerStay2D(Collider2D obj) {

        if (canAttack && Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            HealthController health = obj.gameObject.GetComponent<HealthController>();

            if (health != null) {
                health.changeHealth(-AttackStrength);
                if (health.getIsAlive()) {
                    StartCoroutine("Dmg");
                    Weapon.SetTrigger("Attack");
                }
                
            }

        }

    }

    void OnTriggerEnter2D(Collider2D obj) {

        if (canAttack && Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            HealthController health = obj.gameObject.GetComponent<HealthController>();

            if(health != null) {
                health.changeHealth(-AttackStrength);
                if (health.getIsAlive()) {
                    StartCoroutine("Dmg");
                    Weapon.SetTrigger("Attack");
                }
            }

        }

    }

    public IEnumerator Dmg() {
        canAttack = false;
        yield return new WaitForSeconds(CoolDown);
        canAttack = true;
    }

}
