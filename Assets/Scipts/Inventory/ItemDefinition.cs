using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "TE/ItemDefinition", order = 0)]

// namespace InventoryScripts
// {
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private int MAX_STACK;
    [SerializeField] private int ID;
    [SerializeField] private Sprite ICON;
    [SerializeField] private Sprite NEAR_ICON;
    [SerializeField] private bool IS_PLACABLE;

    public ItemDefinition(int id, int maxStack, Sprite icon, Sprite nearIcon, bool isPlacable)
    {
        ID = id;
        MAX_STACK = maxStack;
        ICON = icon;
        NEAR_ICON = nearIcon;
        IS_PLACABLE = isPlacable;
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
    public Sprite getNearIcon
    {
        get { return NEAR_ICON; }
    }

    public bool getIsPlacable
    {
        get { return IS_PLACABLE; }
    }

}
// }