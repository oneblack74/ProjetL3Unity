using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slot : MonoBehaviour
{
    [SerializeField] private int quantity;
    private Item item = null;

    // Switch les items dans ce slot avec le slot donnée en paramètre
    public void switchItems(Slot slot)
    {
        (Item, int) gotItem = slot.removeItem(slot.getItemQuantity);
        slot.addItem(item, quantity);
        addItem(gotItem.Item1, gotItem.Item2);
    }

    // Retourne la quantité retiré du slot
    public (Item, int) removeItem(int quantity)
    {
        if (quantity >= this.quantity)
        {
            Item tmp = this.item;
            this.quantity = 0;
            this.item = null;
            return (tmp, quantity);
        }
        this.quantity -= quantity;
        return (this.item, quantity);
    }

    // Retourne l'exée d'item qui n'as pas pu être dans le slot
    public (Item, int) addItem(Item item, int quantity)
    {
        if (this.item != null)
        {
            // Switch Item
            return (new Item(0, 0, null), 0);
        }
        this.item = item;
        if (this.quantity + quantity > item.getMaxStack)
        {
            return (this.item, quantity - (item.getMaxStack - this.quantity));
        }
        return (this.item, 0);
    }

    public bool isEmpty()
    {
        return quantity == 0;
    }

    public int getItemQuantity
    {
        get { return quantity; }
    }

    public Item getItem
    {
        get { return item; }
    }
}
