using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// namespace InventoryScripts
// {
public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private int slotsPerLine;
    private List<Slot> tab = new List<Slot>();

    public Inventory(int size, int slotsPerLine)
    {
        this.inventorySize = size;
        this.slotsPerLine = slotsPerLine;
        for (int i = 0; i < inventorySize; i++)
        {
            tab[i] = new Slot(this, i);
        }
    }

    // Retourne le nombre de case par ligne de l'inventaire
    // Retourne -1 si l'offset n'est pas possible (a modifier si besoin)
    public int getNbSlotPerLine()
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
    public int getNbLine()
    {
        if (getNbSlotPerLine() == -1)
        {
            return 1;
        }
        return inventorySize / slotsPerLine;
    }

    public (Item, int) removeItem(int ind, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].getItemQuantity < quantity)
            throw new Exception("Not enough item in the slot");

        return (tab[ind].getItem, tab[ind].removeItem(quantity).Item2);
    }

    // retourne l'exces d'item non ajoute
    public (Item, int) addItem(int ind, Item item, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].getItemQuantity + quantity > item.getMaxStack)
            throw new Exception("Too much item in the slot");

        return (tab[ind].getItem, tab[ind].addItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (Item, int) addItemFast(Item item, int quantity)
    {
        foreach (Slot slot in tab)
        {
            if (slot.isEmpty())
            {
                return slot.addItem(item, quantity);
            }
        }
        return (null, 0);
    }
}
// }