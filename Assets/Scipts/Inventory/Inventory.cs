using System.Collections.Generic;
using UnityEngine;
using System;

// namespace InventoryScripts
// {
public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 5;
    [SerializeField] private int slotsPerLine = 1;
    private GameManager gameManager;
    [SerializeField] public List<Slot> tab = new List<Slot>();

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < inventorySize; i++)
        {
            tab.Add(new Slot(this, i, 0, gameManager.ConvertIdToItem(0)));
            tab[i].addItem(gameManager.ConvertIdToItem(0), 0);
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

    public (ItemDefinition, int) removeItem(int ind, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].getItemQuantity < quantity)
            throw new Exception("Not enough item in the slot");

        return (tab[ind].getItem, tab[ind].removeItem(quantity).Item2);
    }

    // retourne l'exces d'item non ajoute
    public (ItemDefinition, int) addItem(int ind, ItemDefinition item, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[ind].getItemQuantity + quantity > item.getMaxStack)
            throw new Exception("Too much item in the slot");

        //Debug.Log(tab[ind].getItemQuantity);
        return (tab[ind].getItem, tab[ind].addItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (ItemDefinition, int) addItemFast(ItemDefinition item, int quantity)
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

    public void switchItem(int index, Slot slot)
    {
        Debug.Log(index);
        tab[index].switchItems(slot);
    }

    public ItemDefinition checkItem(int index)
    {
        return tab[index].getItem;
    }

    public int checkItemQuantity(int index)
    {
        return tab[index].getItemQuantity;
    }

    public int getInventorySize
    {
        get { return inventorySize; }
    }

    public Slot GetSlot(int index)
    {
        return tab[index];
    }

}
// }