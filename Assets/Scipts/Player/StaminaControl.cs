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


    /**
    * Used to drain stamina ( when casting a dash for example )
    * Will if there is enough stamina to call a drain
    * If true, drains stamina. Returns false otherwise.
    * @param s : Float, stamina to drain
*/
    public bool TryDrainStamina(float s)
    {
        if (this.stamina >= s)
        {
            DrainStamina(s);
            return true;
        }
        else
        {
            Debug.Log("[-] Can't drain stamina " + stamina + " / " + s);
            return false;
        }
    }

    // This function is never called on its own 
    // Useful for 1-time drains, like dashes, jumps..
    public void DrainStamina(float s)
    {
        timer = 0f;
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
            if (stamina < maxStamina) stamina += refillRate * Time.deltaTime;
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
}