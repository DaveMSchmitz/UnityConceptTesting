using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : MonoBehaviour {

    private PlayerAttack atk;
    private HealthController health;
    private PlayerMovementController movement;

    //public bool isDefending;

    // Use this for initialization
    /*void Start() {
        atk = GetComponent<PlayerAttack>();
        health = GetComponent<HealthController>();
        movement = GetComponent<PlayerMovementController>();
    }*/

    private void OnEnable() {
        atk = GetComponent<PlayerAttack>();
        health = GetComponent<HealthController>();
        movement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            Debug.Log("PUSH SHIFT BUTTON");
            atk.AbleAttack(false);
            health.setInvincible(true);
            movement.setSpeed(movement.DefaultMaxSpeed / 2f);
            Debug.Log(movement.DefaultMaxSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            Debug.Log("RELEASE SHIFT BUTTON");
            atk.AbleAttack(true);
            health.setInvincible(false);
            movement.setSpeed(movement.DefaultMaxSpeed);
            Debug.Log(movement.DefaultMaxSpeed);
        }
    }
}
