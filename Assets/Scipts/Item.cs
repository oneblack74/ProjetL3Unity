using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "TE/Item", order = 0)]

// namespace InventoryScripts
// {
public class Item : ScriptableObject
{
    [SerializeField] private int MAX_STACK;
    [SerializeField] private int ID;
    [SerializeField] private Sprite ICON;

    public Item(int id, int maxStack, Sprite icon)
    {
        this.ID = id;
        this.MAX_STACK = maxStack;
        this.ICON = icon;
    }

    public int getID
    {
        get { return ID; }
    }

    public int getMaxStack
    {
        get { return MAX_STACK; }
    }

    public Sprite getIcon
    {
        get { return ICON; }
    }
}
// }