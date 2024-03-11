using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 5;
    [SerializeField] private List<Slot> tab = new();

    void Start()
    {
        if (inventorySize == 0)
		{
            inventorySize = 1;
		} 
        InitSlots();
    }

    public void InitSlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            tab.Add(new Slot(0, GameManager.GetInstance().ConvertIdToItem(0)));
        }
    }

    public (ItemDefinition, int) RemoveItem(int ind, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].GetItemQuantity < quantity)
            throw new Exception("Not enough item in the slot");

        return (tab[ind].GetItem, tab[ind].RemoveItem(quantity).Item2);
    }

    // retourne l'exces d'item non ajoute
    public (ItemDefinition, int) AddItem(int ind, ItemDefinition item, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].GetItemQuantity + quantity > item.getMaxStack)
            throw new Exception("Too much item in the slot");

        return (tab[ind].GetItem, tab[ind].AddItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (ItemDefinition, int) AddItemFast(ItemDefinition item, int quantity)
    {
        foreach (Slot slot in tab)
        {
            if (slot.IsEmpty() || (slot.GetItem == item && slot.GetItemQuantity < item.getMaxStack))
            {
                (ItemDefinition, int) reste = slot.AddItem(item, quantity);
                if (reste.Item2 <= 0)
                {
                    return reste;
                }
                AddItemFast(reste.Item1, reste.Item2);
            }
        }
        return (null, 0);
    }

    public void SwitchItem(int index, Slot slot)
    {
        if (tab[index].GetItem == slot.GetItem)
        {
            (ItemDefinition, int) restant = tab[index].AddItem(slot.GetItem, slot.GetItemQuantity);
            slot.RemoveItem(slot.GetItemQuantity);
            slot.AddItem(restant.Item1, restant.Item2);
            return;
        }
        tab[index].SwitchItems(slot);
    }

    public void LeftClick(int slotID)
    {   
        SwitchItem(slotID, GameManager.GetInstance().GetCursorUI.GetComponent<Inventory>().GetSlot(0));
        // Si pb de reférence ici, le cursor n'est pas actif ;)
    }

    public void RightCLick(int slotID)
    {
        GameObject cursor = GameManager.GetInstance().GetCursorUI;
        Slot slotCursor = cursor.GetComponent<Inventory>().GetSlot(0);
        if (slotCursor.IsEmpty())
        {
            (ItemDefinition, int) restant = tab[slotID].RemoveItem(tab[slotID].GetItemQuantity / 2);
            slotCursor.AddItem(restant.Item1, restant.Item2);
        }
        else if (tab[slotID].IsEmpty() || tab[slotID].GetItem == slotCursor.GetItem)
        {
            (ItemDefinition, int) restant = slotCursor.RemoveItem(1);
            tab[slotID].AddItem(restant.Item1, restant.Item2);
        }
    }

    public void RedefineSlots(List<StructSlot> newTab)
    {
        tab.Clear();
        foreach (StructSlot slot in newTab)
        {
            Slot s = new(slot.itemQuantity, GameManager.GetInstance().ConvertIdToItem(slot.itemID));
            tab.Add(s);
        }
    }


    public ItemDefinition CheckItem(int index)
    {
        if (index < 0 || index >= inventorySize)
        {
            throw new Exception("Index out of range");
        }
        return tab[index].GetItem;
    }

    public int CheckItemQuantity(int index)
    {
        return tab[index].GetItemQuantity;
    }

    public int GetInventorySize
    {
        get { return inventorySize; }
    }

    public Slot GetSlot(int index)
    {
        return tab[index];
    }

    public List<Slot> GetTab
    {
        get { return tab; }
    }

}
// }
