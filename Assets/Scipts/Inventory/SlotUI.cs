using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Image itemIcon;

    void Awake()
    {
        textMeshPro = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemIcon = gameObject.transform.GetChild(1).GetComponent<Image>();
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

    public void LeftClick()
    {

    }

    public void RightCLick()
    {

    }

    public void MiddleClick()
    {

    }
}
