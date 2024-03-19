using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "TE/ItemDefinition", order = 0)]

public class ItemDefinition : ScriptableObject
{
    [Header("Infos")]
    [SerializeField] private int MAX_STACK;
    [SerializeField] private int ID;

    [Header("Icons")]
    [SerializeField] private Sprite ICON;
    [SerializeField] private Sprite NEAR_ICON;

    [Header("Bool")]
    [SerializeField] private bool IS_PLACABLE;

    public ItemDefinition(int id, int maxStack, Sprite icon, Sprite nearIcon, bool isPlacable)
    {
        ID = id;
        MAX_STACK = maxStack;
        ICON = icon;
        NEAR_ICON = nearIcon;
        IS_PLACABLE = isPlacable;
    }

    public int GetID
    {
        get { return ID; }
    }

    public int GetMaxStack
    {
        get { return MAX_STACK; }
    }

    public Sprite GetIcon
    {
        get { return ICON; }
    }
    public Sprite GetNearIcon
    {
        get { return NEAR_ICON; }
    }

    public bool GetIsPlacable
    {
        get { return IS_PLACABLE; }
    }

}