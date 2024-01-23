using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slot : MonoBehaviour
{
    [SerializeField] private int quantity;
    private Item item;

    public (Item, int) removeItem(int quantity)
    {
        return (new Item(0, 0, null), 0);
    }

    public (Item, int) addItem(int quantity)
    {
        return (new Item(0, 0, null), 0);
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
