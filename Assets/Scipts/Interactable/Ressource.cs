using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Ressource : MonoBehaviour, IInteractable
{
    [SerializeField] private int itemID;
    [SerializeField] private int quantity = 1;

    public bool Interact()
    {
        GameManager.GetInstance().GetPlayerController.GetInventory.AddItemFast(GameManager.GetInstance().ConvertIdToItem(itemID), quantity);
        Destroy(gameObject);
        return true;
    }
}
