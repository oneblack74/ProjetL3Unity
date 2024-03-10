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
    private readonly List<SlotUI> slotsUI = new();

    void Awake()
    {
        Init();
    }

    private void Init()
	{
        if (inventory != null)
        {
            if (TryGetComponent<GridLayoutGroup>(out gridLayout))
            {
                AjusterTailleGridLayout();
            }
            else
            {
                Debug.LogError("Le composant GridLayoutGroup n'a pas été trouvé sur cet objet.");
            }
            InstantiateSlots();
        }
    }

    void Update()
    {
        if (inventory != null)
        {
            UpdateUI();
        }
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
                slotsUI[i].LinkInventory(inventory);
                slotsUI[i].SetId(tmp);
            }
        }
    }

    public void LinkInventory(Inventory inv)
	{
        Debug.Log("Linking Inventory...");
        if (inventory == null)
        {
            Debug.Log(inv);
            inventory = inv;
            Init();
            Debug.Log("Inventory Linked");
            return;
        }
        Debug.Log("Inventory Not Linked");
	}

    private void AjusterTailleGridLayout()
    {
        gridLayout.spacing = new Vector2(espaceEntreElements, espaceEntreElements);
        float size = (GetComponent<RectTransform>().rect.width - (espaceEntreElements * (inventory.GetNbSlotPerLine() - 1))) / inventory.GetNbSlotPerLine();
        gridLayout.cellSize = new Vector2(size, size);
    }
}
