using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Ressource : MonoBehaviour, IInteractable
{
    [SerializeField] private int itemID;
    [SerializeField] private int quantity = 1;
    [SerializeField] private List<int> requieredItemIDs = new();
    GameManager manager;

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public bool Interact()
    {
        for (int i = 0; i < requieredItemIDs.Count; i++)
        {
            if (!manager.GetPlayerController.GetInventory.IsInInventory(requieredItemIDs[i]))
            {
                return false;
            }
        }
        if (!manager.GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().IsFullFor(manager.ConvertIdToItem(itemID), quantity))
        {
            manager.GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().AddItemFast(manager.ConvertIdToItem(itemID), quantity);
        }
        else
        {
            manager.GetPlayerController.GetInventory.AddItemFast(manager.ConvertIdToItem(itemID), quantity);
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
