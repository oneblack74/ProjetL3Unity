using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Image itemIcon;
    private int slotID;
    private Inventory inventoryLink;

    void Awake()
    {
        textMeshPro = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemIcon = gameObject.transform.GetChild(1).GetComponent<Image>();
    }

    // Le gO doit avoir un Inventory, c'est celui ci qui va être utilisé pour le slotUI
    public void LinkInventory(Inventory inv)
    {
        inventoryLink = inv;
    }

    public void UpdateUI(ItemDefinition item, int itemQuantity)
    {
        if (item == null)
        {
            return;
        }
        itemIcon.sprite = item.getIcon;

        if (textMeshPro == null)
        {
            Debug.Log("TextMeshPro Not found");
            return;
        }
        if (itemQuantity == 0)
            textMeshPro.text = "";
        else
            textMeshPro.text = itemQuantity.ToString();
    }

    public void SetId(int id)
    {
        slotID = id;
    }

    public void LeftClick()
    {
        inventoryLink.LeftClick(slotID);
    }

    public void RightCLick()
    {
        inventoryLink.RightCLick(slotID);
    }

    public void MiddleClick()
    {

    }
}
