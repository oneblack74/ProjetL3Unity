using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject PrefabUi;
    [SerializeField] private int id;
    private GameObject actualUI;
    private bool openned = false;

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
    }

    private void Close()
    {
        Destroy(actualUI);
    }

    public int GetID
    {
        get { return id; }
    }
}
