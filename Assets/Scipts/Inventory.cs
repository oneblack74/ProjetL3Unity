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

    public Tuple<Item,int> removeItem(int ind, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new OutOfBoundException("Index out of range");
        if (tab[ind].getItemQuantity < quantity)
            throw new NotEnoughItemException("Not enough item in the slot");
        
        return new Tuple(tab[ind].getItem, tab[ind].removeItem(quantity));
    }

    // retourne l'exces d'item non ajoute
    public Tuple<Item,int> addItem(int ind, Item item, int quantity)
    {
        if (ind < 0 || ind >= inventorySize)
            throw new OutOfBoundException("Index out of range");
        if (tab[ind].getItemQuantity + quantity > item.getMaxStack)
            throw new TooMuchItemException("Too much item in the slot");
        
        return new Tuple(tab[ind].getItem, tab[ind].addItem(quantity));
    }
}
