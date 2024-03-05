using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(StaminaControl))]
public class Sprint : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float staminaConsumption;

    public void Sprinting(bool isSprinting, StaminaControl staminaControl, Movement movement)
    {
        if (isSprinting)
        {
            if (staminaControl.GetStamina > 0)
            {
                movement.GetSpeed = movement.GetSpeedTmp * speedMultiplier;
                staminaControl.DrainStaminaOverTime(staminaConsumption);
            }
            else
            {
                movement.GetSpeed = movement.GetSpeedTmp;
            }
        }
    }
}
