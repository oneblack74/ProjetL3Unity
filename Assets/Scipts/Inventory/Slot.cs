using UnityEngine;
public class Slot
{
    private int quantity = 0;
    private ItemDefinition item = null;
    private GameManager manager;

    public Slot(int quantity = 0, ItemDefinition item = null)
    {
        this.quantity = quantity;
        this.item = item;
        InitManager(GameManager.GetInstance());
    }

    public void InitManager(GameManager manager)
    {
        this.manager = manager;
    }

    // Switch les items dans ce slot avec le slot donnée en paramètre
    public void SwitchItems(Slot slot)
    {
        (ItemDefinition, int) gotItem = slot.RemoveItem(slot.GetItemQuantity);
        slot.AddItem(item, quantity);
        AddItem(gotItem.Item1, gotItem.Item2);
    }

    // Retourne la quantité retiré du slot
    public (ItemDefinition, int) RemoveItem(int quantity)
    {
        if (quantity >= this.quantity)
        {
            ItemDefinition tmp = item;
            this.quantity = 0;
            item = manager.ConvertIdToItem(0);
            return (tmp, quantity);
        }
        this.quantity -= quantity;
        return (item, quantity);
    }

    // Retourne le trop d'item qui n'as pas pu être dans le slot
    public (ItemDefinition, int) AddItem(ItemDefinition item, int quantity)
    {
        if (this.item == item)
        {
            if (this.quantity + quantity > item.GetMaxStack)
            {
                int restant = quantity - (item.GetMaxStack - this.quantity);
                this.quantity = item.GetMaxStack;
                return (this.item, restant);
            }
            Debug.Log("Before quantity : " + this.quantity + " adding " + quantity);
            this.quantity += quantity;
            Debug.Log(this.quantity);
            return (this.item, 0);
        }
        this.quantity = quantity;
        if (quantity == 0)
        {
            this.item = manager.ConvertIdToItem(0);
        }
        else
        {
            this.item = item;
        }
        return (this.item, 0);
    }

    public string GetNbToDraw()
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

    public bool IsEmpty()
    {
        return quantity == 0;
    }

    public int GetItemQuantity
    {
        get { return quantity; }
    }

    public ItemDefinition GetItem
    {
        get { return item; }
    }
}