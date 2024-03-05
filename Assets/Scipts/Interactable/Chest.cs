using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject PrefabUi;
    private GameObject actualUI;
    private bool openned = false;

	public int Interact()
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
        return 1;
    }

    private void Open()
    {
        actualUI = Instantiate(PrefabUi);
        Transform parent = GameObject.Find("UI").transform;
        Debug.Log(parent);
        actualUI.transform.SetParent(parent, false);
        actualUI.transform.SetSiblingIndex(0);
        actualUI.transform.GetChild(0).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(gameObject.GetComponent<Inventory>());
        actualUI.transform.GetChild(1).transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(GameManager.GetInstance().GetPlayerController.GetInventory);

    }

    private void Close()
	{
        Debug.Log("Closed");
        Destroy(actualUI);
	}
}
