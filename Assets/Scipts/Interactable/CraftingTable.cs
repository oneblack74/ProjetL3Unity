using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject prefabUI;
    [SerializeField] private Recipe[] recipes;
    private GameManager manager;
    private GameObject actualUI;
    private bool openned = false;
    private Inventory craftingInventory;

    void Awake()
    {
        craftingInventory = gameObject.GetComponent<Inventory>();
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public bool Interact()
    {
        if (!manager.GetInMenu)
        {
            if (openned)
            {
                Close();
                manager.CloseInventory();
            }
            else
            {

                Open();
                manager.OpenInventory();

            }
            openned = !openned;
            return true;
        }
        return false;
    }

    private void Open()
    {
        actualUI = Instantiate(prefabUI);
        Transform parent = GameObject.Find("UI").transform;
        actualUI.transform.SetParent(parent, false);
        actualUI.transform.SetSiblingIndex(0);
        actualUI.transform.GetChild(0).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(gameObject.GetComponent<Inventory>());
        actualUI.transform.GetChild(1).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(manager.GetPlayerController.GetInventory);

        actualUI.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Craft);

    }

    private void Close()
    {
        Inventory playerInventory = manager.GetPlayerController.GetInventory;
        for (int i = 0; i < 3; i++)
        {
            if (craftingInventory.CheckItem(i).GetID != 0)
            {
                playerInventory.AddItemFast(manager.ConvertIdToItem(craftingInventory.CheckItem(i).GetID), craftingInventory.CheckItemQuantity(i));
                craftingInventory.RemoveItem(i, craftingInventory.CheckItemQuantity(i));
            }
        }

        Destroy(actualUI);
    }

    public void Craft()
    {
        foreach (Recipe recipe in recipes)
        {
            if (craftingInventory.CheckItem(0).GetID == recipe.GetItem0.id && craftingInventory.CheckItemQuantity(0) >= recipe.GetItem0.quantity &&
                craftingInventory.CheckItem(1).GetID == recipe.GetItem1.id && craftingInventory.CheckItemQuantity(1) >= recipe.GetItem1.quantity &&
                craftingInventory.CheckItem(2).GetID == recipe.GetItem2.id && craftingInventory.CheckItemQuantity(2) >= recipe.GetItem2.quantity)
            {
                manager.PlaySound("Craft");
                craftingInventory.RemoveItem(0, recipe.GetItem0.quantity);
                craftingInventory.RemoveItem(1, recipe.GetItem1.quantity);
                craftingInventory.RemoveItem(2, recipe.GetItem2.quantity);
                manager.GetPlayerController.GetInventory.AddItemFast(manager.ConvertIdToItem(recipe.GetResult.id), recipe.GetResult.quantity);
            }
        }
    }
}
