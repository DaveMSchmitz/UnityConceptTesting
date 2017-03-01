using UnityEngine;

public class HealthControllerStatic : MonoBehaviour
{

    public static int health;
    public static int maxHealth;
    public static bool isAlive;

    public HealthControllerStatic(int mHealth)
    {
        //set the health to max health
        health = maxHealth;
        maxHealth = mHealth;

        //a check to make sure that the health is bigger than zero
        if (health > 0)
        {
            isAlive = true;
        }
    }

    public HealthControllerStatic(int mHealth, int h)
    {
        //set the health to max health
        health = h;
        maxHealth = mHealth;

        //a check to make sure that the health is bigger than zero
        if (health > 0)
        {
            isAlive = true;
        }
    }

    public bool setHealth(int h)
    {
        //set the health
        health = h;

        //if the trying to set health past max health, set to max health instead
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //if the health is less than or equal to zero change the object to dead
        if (health <= 0)
        {
            isAlive = false;
        }

        return isAlive;
    }

    //returns the current health
    public int getCurrentHealth()
    {
        return health;
    }

    //sets a new maxHealth
    public bool setMaxHealth(int mHealth)
    {
        //set the new maxHealth
        maxHealth = mHealth;

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

    //returns weather the object is dead or alive.
    public bool getIsAlive()
    {
        return isAlive;
    }


}
