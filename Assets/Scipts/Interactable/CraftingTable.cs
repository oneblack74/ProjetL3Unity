using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject PrefabUi;
    [SerializeField] private Recipe[] recipes;
    private GameManager manager;
    private GameObject actualUI;
    private bool openned = false;
    private Inventory inventory;

    void Awake()
    {
        Inventory craftingInventory = gameObject.GetComponent<Inventory>();
        manager = GameManager.GetInstance();
    }

    public bool Interact()
    {
        if (openned)
        {
            Close();
            GameManager.GetInstance().CloseInventory();
        }
        else
        {
            Open();
            GameManager.GetInstance().OpenInventory();
        }
        openned = !openned;
        return true;
    }

    private void Open()
    {
        actualUI = Instantiate(PrefabUi);
        Transform parent = GameObject.Find("UI").transform;
        actualUI.transform.SetParent(parent, false);
        actualUI.transform.SetSiblingIndex(0);
        actualUI.transform.GetChild(0).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(gameObject.GetComponent<Inventory>());
        actualUI.transform.GetChild(1).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(GameManager.GetInstance().GetPlayerController.GetInventory);

        PrefabUi.transform.Find("BtnCraft").GetComponent<Button>().onClick.AddListener(Close);

    }

    private void Close()
    {
        Destroy(actualUI);
    }

    public void Craft()
    {
        Inventory playerInventory = manager.GetPlayerController.GetInventory;

        foreach (Recipe recipe in recipes)
        {
            if (playerInventory.CheckItem(0).getID == recipe.GetItem0.id && playerInventory.CheckItemQuantity(0) >= recipe.GetItem0.quantity &&
                playerInventory.CheckItem(1).getID == recipe.GetItem1.id && playerInventory.CheckItemQuantity(1) >= recipe.GetItem1.quantity &&
                playerInventory.CheckItem(2).getID == recipe.GetItem2.id && playerInventory.CheckItemQuantity(2) >= recipe.GetItem2.quantity)
            {
                playerInventory.RemoveItem(0, recipe.GetItem0.quantity);
                playerInventory.RemoveItem(1, recipe.GetItem1.quantity);
                playerInventory.RemoveItem(2, recipe.GetItem2.quantity);
                playerInventory.AddItemFast(manager.ConvertIdToItem(recipe.GetResult.id), recipe.GetResult.quantity);
            }
        }
    }
}
