using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject curorUI;
    [SerializeField] private GameManager gameManager;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        inventory.AddItemFast(gameManager.ConvertIdToItem(1), 10);
    }

    public void ShowInventory(bool b)
    {
        inventoryUI.SetActive(b);
        curorUI.SetActive(b);
    }

}
