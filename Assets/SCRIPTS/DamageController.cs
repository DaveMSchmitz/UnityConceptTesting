using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    private HealthController health;
    private Coroutine ambientDamageCoroutine;
    private Coroutine enemyDamageCoroutine;
    public LayerMask Damageable;

    [SerializeField]
    private int AttackStrength = 1;

    [SerializeField]
    private int CoolDown = 1;

    private float nextFire = 0;
    private float fireRate = 1f;

    public bool isAttacking;

    // Use this for initialization
    void Start() {
        health = GetComponent<HealthController>();
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            isAttacking = true;
        }
    }

    void OnTriggerStay2D(Collider2D obj) {
        if (isAttacking) {
            if (obj.tag == "Damageable") {

                obj.GetComponent<HealthController>().changeHealth(-1);
                Debug.Log("ATTACKING ENEMY: ENEMY HEALTH: " + obj.GetComponent<HealthController>().getCurrentHealth());
                isAttacking = false;
            }
        }

        if (obj.tag == "Damage") {
            health.changeHealth(-1);
        }
        if (obj.tag == "Enemy") {
            health.changeHealth(-1);
        }

    }

    void OnTriggerEnter2D(Collider2D obj) {

        if (Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            //put damage code here
        }

        if (obj.tag == "Damage") {
            health.changeHealth(-1);
        }
        if (obj.tag == "Enemy") {
            health.changeHealth(-1);
        }

    }
}
