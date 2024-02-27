using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private float espaceEntreElements = 10f;
    [SerializeField] private SlotUI slotUIPrefab;
    private GridLayoutGroup gridLayout;
    [SerializeField] private Inventory inventory;
    private List<SlotUI> slotsUI = new List<SlotUI>();

    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();

        if (gridLayout != null)
        {
            AjusterTailleGridLayout();
        }
        else
        {
            Debug.LogError("Le composant GridLayoutGroup n'a pas été trouvé sur cet objet.");
        }

        InstantiateSlots();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.GetInventorySize; i++)
        {
            slotsUI[i].UpdateUI(inventory.CheckItem(i), inventory.CheckItemQuantity(i));
        }
    }

    private void InstantiateSlots()
    {
        if (slotUIPrefab != null)
        {
            for (int i = 0; i < inventory.GetInventorySize; i++)
            {
                int tmp = i;
                slotsUI.Add(Instantiate(slotUIPrefab, transform));
                slotsUI[i].SetId(tmp);
            }
        }
    }

    // private void OnButtonCLick(int index, GameObject cursor)
    // {
    //     inventory.SwitchItem(index, cursor.GetComponent<Inventory>().GetSlot(0));
    // }

    private void AjusterTailleGridLayout()
    {
        gridLayout.spacing = new Vector2(espaceEntreElements, espaceEntreElements);
        float size = (GetComponent<RectTransform>().rect.width - (espaceEntreElements * (inventory.GetNbSlotPerLine() - 1))) / inventory.GetNbSlotPerLine();
        gridLayout.cellSize = new Vector2(size, size);
    }
}
