using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private LayerMask Damageable;

    [SerializeField]
    private int AttackStrength = 1;

    [SerializeField]
    private float CoolDown = .5f;

    private bool canAttack = true;
    private bool attacked;

    //private GameObject sword;
    private Transform sword;
    private Quaternion originalRot;

	// Use this for initialization
	void OnEnable() {
        canAttack = true;
        attacked = false;
	}

    void Start()
    {
        sword = transform.Find("Sword");
        originalRot = sword.rotation;
    }

    // Update is called once per frame
    void Update () {
        if (canAttack && Input.GetButtonDown("Fire1")) {
            StartCoroutine("grace");
            StartCoroutine("coolDown");
            StartCoroutine("swing");
        }
    }

    void OnTriggerStay2D(Collider2D obj) {

        //if i can attack and the object is inside of the damage layermask, then 
        //attack this enemy and make sure to end the grace period
        if (attacked && Damageable == (Damageable | (1 << obj.gameObject.layer))) {
            obj.gameObject.GetComponent<HealthController>().changeHealth(-AttackStrength);
            Debug.Log(obj.gameObject.GetComponent<HealthController>().getCurrentHealth());
            attacked = false;
        }

    }

    void OnTriggerEnter2D(Collider2D obj) {

        //if i can attack and the object is inside of the damage layermask, then 
        //attack this enemy and make sure to end the grace period
        if (attacked && Damageable == (Damageable | (1 << obj.gameObject.layer))) {

            obj.gameObject.GetComponent<HealthController>().changeHealth(-AttackStrength);
            Debug.Log(obj.gameObject.GetComponent<HealthController>().getCurrentHealth());
            attacked = false;
        }

    }

    private IEnumerator grace() {
        attacked = true;
        yield return new WaitForSeconds(.1f);
        attacked = false;
    }

    private IEnumerator coolDown() {
        canAttack = false;
        yield return new WaitForSeconds(CoolDown);
        canAttack = true;

    }

    private IEnumerator swing() {
        sword.Rotate(0, 0, -45);
        yield return new WaitForSeconds(1);
        sword.Rotate(0, 0, 45);
    }
}
