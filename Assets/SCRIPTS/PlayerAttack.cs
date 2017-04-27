using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private LayerMask Damageable;

    [SerializeField]
    private int AttackStrength = 1;
    private int DefaultAttackStrength = 1;

    [SerializeField]
    private float CoolDown = .5f;

    [SerializeField]
    private Animator Weapon;

    private bool attacked;
    private float time;

	// Use this for initialization
	void OnEnable() {
        attacked = false;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if ((Time.time >= time + CoolDown) && Input.GetButton("Fire1")) {
            StartCoroutine("grace");
            Weapon.SetTrigger("Attack");
            time = Time.time;
        }
    }

    public void AbleAttack(bool atk)
    {
        canAttack = atk;
    }

    void OnTriggerStay2D(Collider2D obj) {

        //if i can attack and the object is inside of the damage layermask, then 
        //attack this enemy and make sure to end the grace period
        if (attacked && Damageable == (Damageable | (1 << obj.gameObject.layer))) {

            HealthController health = obj.gameObject.GetComponent<HealthController>();

            if (health != null) {
                health.changeHealth(-AttackStrength);
                Debug.Log(obj.gameObject.GetComponent<HealthController>().getCurrentHealth());
            }
            attacked = false;
        }

    }

    void OnTriggerEnter2D(Collider2D obj) {

        //if i can attack and the object is inside of the damage layermask, then 
        //attack this enemy and make sure to end the grace period
        if (attacked && Damageable == (Damageable | (1 << obj.gameObject.layer))) {

            HealthController health = obj.gameObject.GetComponent<HealthController>();

            if (health != null) {
                health.changeHealth(-AttackStrength);
                Debug.Log(obj.gameObject.GetComponent<HealthController>().getCurrentHealth());
            }


            attacked = false;
        }

    }

    private IEnumerator grace() {
        attacked = true;
        yield return new WaitForSeconds(.1f);
        attacked = false;
    }

}
