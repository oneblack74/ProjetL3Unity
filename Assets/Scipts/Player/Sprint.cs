using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float staminaConsumption;

    public void Sprinting(bool isSprinting, StaminaControl staminaControl, Movement movement)
    {
        if (isSprinting)
        {
            if (staminaControl.GetStamina > 0)
            {
                movement.GetSpeed = movement.GetSpeedTmp * sprintMultiplier;
                staminaControl.DrainStaminaOverTime(staminaConsumption);
            }
            else
            {
                movement.GetSpeed = movement.GetSpeedTmp;
            }
        }
        else
        {
            movement.GetSpeed = movement.GetSpeedTmp;
        }
    }
}
