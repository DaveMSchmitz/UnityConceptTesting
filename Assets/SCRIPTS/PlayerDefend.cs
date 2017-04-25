using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : MonoBehaviour {

    private PlayerAttack atk;
    private HealthController health;
    private PlayerMovementController movement;

    public bool isDefending;

	// Use this for initialization
	
    void Start () {
        atk = this.GetComponent<PlayerAttack>();
        health = this.GetComponent<HealthController>();
        movement = this.GetComponent<PlayerMovementController>();
    }
    
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //StartCoroutine("block");
            Debug.Log("PUSH SHIFT BUTTON");
            atk.AbleAttack(false);
            health.setInvincible(true);
            movement.multSpeed(.5f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("RELEASE SHIFT BUTTON");
            //StopCoroutine("block");
            atk.AbleAttack(true);
            health.setInvincible(false);
            movement.multSpeed(2f);
        }
        if (!health.getIsAlive())
        {
            atk.AbleAttack(true);
            health.setInvincible(false);
            movement.restoreSpeed();
        }
    }

    /*
    private IEnumerator block()
    {
        atk.AbleAttack(false);
        health.setInvincible(true);
        yield return new WaitForSeconds(0);
        atk.AbleAttack(true);
        health.setInvincible(false);
    }
    */
}
