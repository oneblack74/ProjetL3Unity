using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private int quantity;
    private Item item;
    
    public removeItem(int quantity)
    {
    }


    public addItel(int quantity)
    {

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
