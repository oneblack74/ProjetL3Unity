using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cursorUI;
    [SerializeField] private GameManager gameManager;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        inventory.AddItemFast(gameManager.ConvertIdToItem(1), 10);
    }

    public void ShowInventory(bool b)
    {
        cursorUI.SetActive(b);
        inventoryUI.SetActive(b);
    }

}
