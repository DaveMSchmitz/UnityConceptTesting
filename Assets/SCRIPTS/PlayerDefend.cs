using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : MonoBehaviour {

    [SerializeField]
    private GameObject Shield;

    private PlayerAttack atk;
    private HealthController health;
    private PlayerMovementController movement;
    private Animator shieldAnimator;

    private bool isDefending = false;

    private void OnEnable() {
        atk = GetComponent<PlayerAttack>();
        health = GetComponent<HealthController>();
        movement = GetComponent<PlayerMovementController>();
        shieldAnimator = Shield.GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            //Debug.Log("PUSH SHIFT BUTTON");
            atk.AbleAttack(false);
            health.setInvincible(true);
            movement.setSpeed(movement.DefaultMaxSpeed / 2f);
            Debug.Log(movement.DefaultMaxSpeed);
            isDefending = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            //Debug.Log("RELEASE SHIFT BUTTON");
            atk.AbleAttack(true);
            health.setInvincible(false);
            movement.setSpeed(movement.DefaultMaxSpeed);
            Debug.Log(movement.DefaultMaxSpeed);
            isDefending = false;
        }

        shieldAnimator.SetBool("Blocking", isDefending);
    }
}
