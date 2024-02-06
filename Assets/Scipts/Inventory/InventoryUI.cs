using System.Collections.Generic;
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

        InstantiateSlot();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.getInventorySize; i++)
        {
            //Debug.Log(inventory.checkItemQuantity(i));
            slotsUI[i].UpdateUI(inventory.checkItem(i), inventory.checkItemQuantity(i));
        }
    }

    private void InstantiateSlot()
    {
        GameObject cursor = GameObject.Find("CursorUI");

        if (slotUIPrefab != null)
        {
            for (int i = 0; i < inventory.getInventorySize; i++)
            {
                slotsUI.Add(Instantiate(slotUIPrefab, transform));
                slotsUI[i].GetComponent<Button>().onClick.AddListener(delegate
                {
                    inventory.switchItem(i, cursor.GetComponent<Inventory>().GetSlot(0));
                });
            }
        }
    }

    private void AjusterTailleGridLayout()
    {
        gridLayout.spacing = new Vector2(espaceEntreElements, espaceEntreElements);
        float size = (GetComponent<RectTransform>().rect.width -
        (espaceEntreElements * (inventory.getNbSlotPerLine() - 1))) / inventory.getNbSlotPerLine();
        gridLayout.cellSize = new Vector2(size, size);
    }
}
