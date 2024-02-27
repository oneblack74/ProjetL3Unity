using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TrapSlow : MonoBehaviour
{
    private enum TrapState { Active, Inactive };
    [SerializeField] private TrapState state = TrapState.Active;
    private GameManager manager;
    [SerializeField] private float slowMultiplier = 0.5f;
    private float timer = 0.0f;
    [SerializeField] private float slowDuration = 3.0f;
    private bool timerActive = false;

    void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    void Update()
    {
        if (timerActive)
        {
            UpdateTimerSlow();
        }
    }

    private void UpdateTimerSlow()
    {
        if (timer < slowDuration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            manager.GetPlayerController.GetMovement.GetSpeed = manager.GetPlayerController.GetMovement.GetSpeedTmp;
            timer = 0;
            timerActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == TrapState.Active)
            {
                manager.GetPlayerController.GetMovement.GetSpeed = manager.GetPlayerController.GetMovement.GetSpeedTmp * slowMultiplier;
                state = TrapState.Inactive;
                timerActive = true;
            }

        }
    }
}
