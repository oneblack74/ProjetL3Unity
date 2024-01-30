using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    private Inventory inventory;
    public ItemDefinition item;
    public ItemDefinition item2;

    void Start()
    {
        inventory = new Inventory(2, 2);
        inventory.addItemFast(item, 1);
        inventory.addItemFast(item2, 5);
        Debug.Log(inventory.checkItem(0));
        Debug.Log(inventory.checkItem(1));
        Debug.Log(inventory.checkItemQuantity(1));
    }
}
