using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private float espaceEntreElements = 10f;
    [SerializeField] private GameObject prefabRef;
    private GridLayoutGroup gridLayout;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {

    }


    void AjusterTailleGridLayout()
    {
        gridLayout.spacing = new Vector2(espaceEntreElements, espaceEntreElements);
        float size = (GetComponent<RectTransform>().rect.width - (espaceEntreElements * (inventory.getNbSlotPerLine() - 1))) / inventory.getNbSlotPerLine();
        gridLayout.cellSize = new Vector2(size, size);
    }
}
