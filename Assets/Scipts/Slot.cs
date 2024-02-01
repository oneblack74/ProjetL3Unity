// namespace InventoryScripts
// {
public class Slot
{
    private int quantity = 0;
    private ItemDefinition item = null;
    private Inventory parentInv;
    private int indexInv;

    public Slot(Inventory parentInv, int indexInv, int quantity = 0, ItemDefinition item = null)
    {
        this.quantity = quantity;
        this.item = item;
        this.parentInv = parentInv;
        this.indexInv = indexInv;
    }

    // Switch les items dans ce slot avec le slot donnée en paramètre
    public void switchItems(Slot slot)
    {
        (ItemDefinition, int) gotItem = slot.removeItem(slot.getItemQuantity);
        slot.addItem(item, quantity);
        addItem(gotItem.Item1, gotItem.Item2);
    }

    // Retourne la quantité retiré du slot
    public (ItemDefinition, int) removeItem(int quantity)
    {
        if (quantity >= this.quantity)
        {
            ItemDefinition tmp = item;
            this.quantity = 0;
            item = null;
            return (tmp, quantity);
        }
        this.quantity -= quantity;
        return (item, quantity);
    }

    // Retourne le trop d'item qui n'as pas pu être dans le slot
    public (ItemDefinition, int) addItem(ItemDefinition item, int quantity)
    {
        if (this.item == item)
        {
            if (this.quantity + quantity > item.getMaxStack)
            {
                this.quantity = item.getMaxStack;
                return (this.item, quantity - (item.getMaxStack - this.quantity));
            }
            this.quantity += quantity;
            return (this.item, 0);
        }

        if (this.item != null)
        {
            // Switch Item
        }
        this.item = item;
        this.quantity = quantity;
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

    public ItemDefinition getItem
    {
        get { return item; }
    }
}
// }