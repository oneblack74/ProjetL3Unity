using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Ressource : MonoBehaviour, IInteractable
{
    [SerializeField] private int itemID;
    [SerializeField] private int quantity = 1;
    [SerializeField] private List<int> requieredItemIDs = new();

    public bool Interact()
    {
        for (int i = 0; i < requieredItemIDs.Count; i++)
        {
            if (!GameManager.GetInstance().GetPlayerController.GetInventory.IsInInventory(requieredItemIDs[i]))
            {
                return false;
            }
        }
        if (!GameManager.GetInstance().GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().IsFullFor(GameManager.GetInstance().ConvertIdToItem(itemID), quantity))
        {
            GameManager.GetInstance().GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().AddItemFast(GameManager.GetInstance().ConvertIdToItem(itemID), quantity);
        }
        else
        {
            GameManager.GetInstance().GetPlayerController.GetInventory.AddItemFast(GameManager.GetInstance().ConvertIdToItem(itemID), quantity);
        }
        Destroy(gameObject);
        return true;
    }

    public int GetitemID
    {
        get { return itemID; }
        set { itemID = value; }
    }

    public int GetQuantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
}
