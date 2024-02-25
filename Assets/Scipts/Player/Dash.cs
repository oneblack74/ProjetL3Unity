using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(StaminaControl))]
public class Dash : MonoBehaviour
{
    private Movement movement;
    private StaminaControl staminaControl;
    private float speedMultiplier;
    [SerializeField] private float timerMax;
    [SerializeField] private float distance;
    [SerializeField] private bool isDashing = false;
    private float timer;

    void Awake()
    {
        movement = GetComponent<Movement>();
        staminaControl = GetComponent<StaminaControl>();
        CalculSpeedMultiplier();
    }

    private void CalculSpeedMultiplier()
    {
        float timeNeeded = distance / movement.GetSpeedTmp;
        speedMultiplier = timerMax / timeNeeded;
    }

    public void ActiveDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            timer = 0;
            movement.GetSpeed = movement.GetSpeedTmp * speedMultiplier;
        }
    }

    void Update()
    {
        if (isDashing)
        {
            if (timer < timerMax)
            {
                timer += Time.deltaTime;
            }
            else
            {
                movement.GetSpeed = movement.GetSpeedTmp;
                isDashing = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            switch (movement.GetMyDirToInt())
            {
                case 0:
                    movement.Move(new Vector3(0, 1, 0));
                    break;
                case 1:
                    movement.Move(new Vector3(0, -1, 0));
                    break;
                case 2:
                    movement.Move(new Vector3(-1, 0, 0));
                    break;
                case 3:
                    movement.Move(new Vector3(1, 0, 0));
                    break;
                default:
                    break;
            }
        }
    }
}
