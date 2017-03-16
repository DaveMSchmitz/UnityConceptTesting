using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthController : MonoBehaviour 
{

    public int health;
    public int maxHealth;
    public bool isAlive;

    public HealthController(int maxHealth)
    {
        //set the health to max health
        this.health = maxHealth;
        this.maxHealth = maxHealth;

        //a check to make sure that the health is bigger than zero
        if (health > 0)
        {
            isAlive = true;
        }
    }

    public HealthController(int maxHealth, int health)
    {
        //set the health to max health
        this.health = health;
        this.maxHealth = maxHealth;

        //a check to make sure that the health is bigger than zero
        if (health >= 0)
        {
            isAlive = true;
        }
    }

    public bool setHealth(int health)
    {
        //set the health
        this.health = health;

        //if the trying to set health past max health, set to max health instead
        if (health > maxHealth)
        {
            this.health = maxHealth;
        }

        //if the health is less than or equal to zero change the object to dead
        if (health <= 0)
        {
            isAlive = false;
        } else
        {
            isAlive = true;
        }

        return isAlive;
    }

    //returns the current health
    public int getCurrentHealth()
    {
        return health;
    }

    //sets a new maxHealth
    public bool setMaxHealth(int maxHealth)
    {
        //set the new maxHealth
        this.maxHealth = maxHealth;

        //if the current health is higher than the new max health, set the new health to maxHealth
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        return isAlive;
    }

    //returns the max health
    public int getMaxHealth()
    {
        return maxHealth;
    }

    //changes the health by what ever the change in health is
    public bool changeHealth(int deltaHealth)
    {
        //change the health by delta
        health += deltaHealth;

        //check if the object is dead
        if (health <= 0)
        {
            isAlive = false;
        }

        return isAlive;
    }

    //takes all health and sets to dead
    public bool kill()
    {
        health = 0;
        isAlive = false;

        return isAlive;
    }

    //returns whether the object is dead or alive.
    public bool getIsAlive()
    {
        return isAlive;
    }
     

}
