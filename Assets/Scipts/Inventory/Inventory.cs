using System.Collections.Generic;
using UnityEngine;
using System;

// namespace InventoryScripts
// {
public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 5;
    [SerializeField] private int slotsPerLine = 1;
    [SerializeField] public List<Slot> tab = new();

    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            tab.Add(new Slot(0, GameManager.GetInstance().ConvertIdToItem(0)));
        }
    }

    // Retourne le nombre de case par ligne de l'inventaire
    // Retourne -1 si l'offset n'est pas possible (a modifier si besoin)
    public int GetNbSlotPerLine()
    {
        if (slotsPerLine == 0)
        {
            return inventorySize;
        }
        if (inventorySize % slotsPerLine == 0)
        {
            return slotsPerLine;
        }
        return -1;
    }

    // Retourne le nombre de lignes de l'inventaire
    public int GetNbLine()
    {
        if (GetNbSlotPerLine() == -1)
        {
            return 1;
        }
        return inventorySize / slotsPerLine;
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

        //Debug.Log(tab[ind].getItemQuantity);
        return (tab[ind].GetItem, tab[ind].AddItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (ItemDefinition, int) AddItemFast(ItemDefinition item, int quantity)
    {
        foreach (Slot slot in tab)
        {
            if (slot.IsEmpty())
            {
                return slot.AddItem(item, quantity);
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
        GameObject cursor = GameObject.Find("CursorUI");
        SwitchItem(slotID, cursor.GetComponent<Inventory>().GetSlot(0));
    }

    public void RightCLick(int slotID)
    {
        GameObject cursor = GameObject.Find("CursorUI");
        Slot slotCursor = cursor.GetComponent<Inventory>().GetSlot(0);
        if (slotCursor.IsEmpty())
        {
            tab[slotID].RemoveItem(tab[slotID].GetItemQuantity / 2);
        }
    }


    public ItemDefinition CheckItem(int index)
    {
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

}
// }