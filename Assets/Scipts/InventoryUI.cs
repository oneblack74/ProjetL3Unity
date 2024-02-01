using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private float espaceEntreElements = 10f;
    [SerializeField] private GameObject prefabRef;
    private GridLayoutGroup gridLayout;
    [SerializeField] private Inventory inventory;


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

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.getInventorySize; i++)
        {
            Debug.Log(inventory.checkItemQuantity(i));
            //transform.GetChild(i).GetComponent<SlotUI>().UpdateUI(inventory.checkItem(i), inventory.checkItemQuantity(i));
        }
    }

    private void InstantiateSlot()
    {
        for (int i = 0; i < inventory.getInventorySize; i++)
        {
            GameObject.Instantiate(prefabRef, transform);
        }
    }

    private void AjusterTailleGridLayout()
    {
        gridLayout.spacing = new Vector2(espaceEntreElements, espaceEntreElements);
        float size = (GetComponent<RectTransform>().rect.width - (espaceEntreElements * (inventory.getNbSlotPerLine() - 1))) / inventory.getNbSlotPerLine();
        gridLayout.cellSize = new Vector2(size, size);
    }
}
