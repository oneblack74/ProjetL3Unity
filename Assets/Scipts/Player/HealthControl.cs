using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthControl : MonoBehaviour
{
    [SerializeField] private float maxHealth = 0f;          //? Maximum health
    [SerializeField] private float health = 0f;              //? Actual health
    [SerializeField] private float regenDelay = 0f;         //? Delay after which the health will recover
    [SerializeField] private float refillRate = 0f;         //? Rate at which the health will recover
    private float timer = 0f;                               //? Used for health regen  


    /**
    * Used to drain health ( when taking damage for example )
    * Will drain if there is enough health 
    * If true, drains health. Returns false otherwise.
    * @param s : Float, health to drain
*/
    public bool TryDrainHealth(float h)
    {
        if (this.health >= h)
        {
            DrainHealth(h);
            return true;
        }
        else
        {
            Debug.Log("[-] Can't drain health " + health + " / " + h);
            return false;
        }
    }

    // This function is never called on its own 
    // Useful for 1-time drains
    public void DrainHealth(float h)
    {
        timer = 0f;
        health -= h;
    }

    // This function is never called on its own
    // Useful for draining over time, like for applying poison
    public void DrainHealthOverTime(float rate)
    {
        timer = 0f;
        health -= rate * Time.deltaTime;
    }

    public void RegenHealth()
    {
        if (this.timer >= regenDelay)
        {
            if (health < maxHealth) health += refillRate * Time.deltaTime;
        }
    }

    public void AddHealth(float amount)
    {
        if (amount + health > maxHealth)
        {
            health = maxHealth;
        } else {
            health += amount;
        }
    }

    private void UpdateTimer()
    {
        if (timer < regenDelay)
        {
            timer += Time.deltaTime;
        }
    }
    void Update()
    {
        UpdateTimer();
        RegenHealth();
    }
}