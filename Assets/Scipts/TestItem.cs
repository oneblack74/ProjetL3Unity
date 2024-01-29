using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public Inventory inventory;
    public Item item;
    
    public void testAddItem()
    {
        inventory.addItem(2, item, 3);
    }
}
