using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// namespace InventoryScripts
// {
public class Slot : MonoBehaviour
{
    private int quantity = 0;
    private Item item = null;
    private Inventory parentInv;
    private int indexInv;
    private Image image;
    private TextMeshPro textMeshPro;
    private GameObject gameObject;

    public Slot(Inventory parentInv, int indexInv, GameObject gameObject, int quantity = 0, Item item = null)
    {
        this.quantity = quantity;
        this.item = item;
        this.parentInv = parentInv;
        this.indexInv = indexInv;

        this.gameObject = gameObject;
        this.gameObject = Instantiate(gameObject, parentInv.transform);

        this.image = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        this.textMeshPro = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
    }


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

    public string getNbToDraw()
    {
        if (quantity == 0 || quantity == 1)
        {
            return "";
        }
        else
        {
            return quantity + "";
        }
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
// }