using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthController : MonoBehaviour {

    [SerializeField]
    private int health = 10;

    [SerializeField]
    private int maxHealth = 10;

    [SerializeField]
    private float InvincibilityTime = 0;

    [SerializeField]
    private bool invincible;
    private Killable myBeing;

    public void Start() {
        myBeing = GetComponent<Killable>();
        invincible = false;
    }

    public void OnEnable() {
        invincible = false;
    }

    public void setInvincible(bool invc)
    {
        invincible = invc;
    }

    public void setHealth(int health) {
        //set the health
        this.health = health;

        //if the trying to set health past max health, set to max health instead
        if (health > maxHealth) {
            this.health = maxHealth;
        }

        //if the health is less than or equal to zero change the object to dead
        if (health <= 0) {
            myBeing.killed();

        } else {
            myBeing.healthChanged();
        }
    }

    //returns the current health
    public int getCurrentHealth() {
        return health;
    }

    //sets a new maxHealth
    public void setMaxHealth(int maxHealth) {
        //set the new maxHealth
        this.maxHealth = maxHealth;

        //if the current health is higher than the new max health, set the new health to maxHealth
        if (health > maxHealth) {
            health = maxHealth;
            myBeing.healthChanged();
        }
    }

    //returns the max health
    public int getMaxHealth() {
        return maxHealth;
    }

    //changes the health by what ever the change in health is
    public void changeHealth(int deltaHealth) {

        if (!invincible) {
            //change the health by delta
            health += deltaHealth;

            //check if the object is dead
            if (health <= 0) {
                myBeing.killed();
            } else {
                myBeing.healthChanged();
                StartCoroutine("waitCoroutine");
            }
        }

    }

    //takes all health and sets to dead
    public void kill() {
        health = 0;
    }

    //returns whether the object is dead or alive.
    public bool getIsAlive() {
        return health > 0;
    }

    private IEnumerator waitCoroutine() {
        invincible = true;

        yield return new WaitForSeconds(InvincibilityTime);

        invincible = false;
    }

}
