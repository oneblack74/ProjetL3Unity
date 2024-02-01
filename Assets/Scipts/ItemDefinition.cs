using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "TE/ItemDefinition", order = 0)]

// namespace InventoryScripts
// {
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private int MAX_STACK;
    [SerializeField] private int ID;
    [SerializeField] private Sprite ICON;

    public ItemDefinition(int id, int maxStack, Sprite icon)
    {
        ID = id;
        MAX_STACK = maxStack;
        ICON = icon;
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