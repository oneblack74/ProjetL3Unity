using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaControl : MonoBehaviour
{
    [SerializeField] private float maxStamina = 0f;      // Maximum Stamina
    [SerializeField] private float stamina = 0f;         // Actual Stamina
    [SerializeField] private float regenPerSecond = 0f;  // Stamina regen / second
    [SerializeField] private float regenDelay = 0f;      // Delay after which the stamina will recover
    [SerializeField] private float refillRate = 0f;      // Rate at which the stamina will recover
    private float timer = 0f;

    public StaminaControl(float maxStamina, float regenPerSecond, float regenDelay, float refillRate) {
        this.maxStamina = maxStamina;
        this.regenPerSecond = regenPerSecond;
        this.regenDelay = regenDelay;
        this.refillRate = refillRate;
    }

    /**
    * Used to drain stamina ( when casting a dash for example )
    * Will if there is enough stamina to call a drain
    * If true, drains stamina. Returns false otherwise.
    * @param s : Float, stamina to drain
    */
    public bool TryDrainStamina(float s) { 
        if (this.stamina >= s) {
            DrainStamina(s);
            return true;
        } else {
            Debug.Log("[-] Can't drain stamina "+stamina+" / "+s);
            return false;
        }
    }

    // This function is never called on its own
    public void DrainStamina(float s) {
        stamina -= s;
    }

    public void RegenStamina() {
        timer = 0f;
        while (stamina < maxStamina) {
            if (timer > 0.05) {
                stamina+= refillRate;
            }
        }
    }

    void Update() {
        timer += Time.deltatime;
        if (timer < regenDelay) { // On attend le delay
            // Délai atteint, on trigger la regen 
            RegenStamina();
            timer = 0f;
        }

    }
}
