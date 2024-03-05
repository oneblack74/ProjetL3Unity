using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TrapVisibility))]
public class TrapDamage : MonoBehaviour
{
    private enum TrapState { Active, Inactive };
    [SerializeField] private TrapState state = TrapState.Active;
    private GameManager manager;
    [SerializeField] private float trapDamage = 10.0f;

    void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == TrapState.Active)
            {
                manager.GetPlayerController.GetHealthControl.RemoveHealth(trapDamage);
                GetComponent<TrapVisibility>().ChangeVisibility();
                state = TrapState.Inactive;
            }

        }
    }
}
