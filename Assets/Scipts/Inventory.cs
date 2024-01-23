using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private int offsetY;
    private List<Slot> tab = new List<Slot>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        return (tab[ind].getItem, tab[ind].addItem(quantity).Item2);
    }
}
