using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaminaControl : MonoBehaviour
{
    [SerializeField] private float maxStamina = 0f;         //? Maximum Stamina
    [SerializeField] private float stamina = 0f;            //? Actual Stamina
    [SerializeField] private float regenDelay = 0f;         //? Delay after which the stamina will recover
    [SerializeField] private float refillRate = 0f;         //? Rate at which the stamina will recover
    private float timer = 0f;                               //? Used for stamina regen  



    // This function is never called on its own 
    // Useful for 1-time drains, like dashes, jumps..
    public void DrainStamina(float s)
    {
        timer = 0f;
        if (stamina >= s)
            stamina -= s;

    }

    // This function is never called on its own
    // Useful for draining over time, like for sprinting
    public void DrainStaminaOverTime(float rate)
    {
        timer = 0f;
        stamina -= rate * Time.deltaTime;
    }

    public void RegenStamina()
    {
        if (this.timer >= regenDelay)
        {
            if (stamina < maxStamina)
            {
                stamina += refillRate * Time.deltaTime;
                if (stamina > maxStamina)
                    stamina = maxStamina;
            }
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
        RegenStamina();
    }

    public void InitValues(float maxStamina, float stamina, float regenDelay, float refillRate)
    {
        this.maxStamina = maxStamina;
        this.stamina = stamina;
        this.regenDelay = regenDelay;
        this.refillRate = refillRate;
    }

    public float GetStamina
    {
        get { return stamina; }
    }

    public float GetMaxStamina
    {
        get { return maxStamina; }
    }

    public float GetRegenDelay
    {
        get { return regenDelay; }
    }

    public float GetRefillRate
    {
        get { return refillRate; }
    }
}