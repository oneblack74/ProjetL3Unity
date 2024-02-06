using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject inventoryUI;

    void Start()
    {
        inventoryUI = GameObject.Find("InventoryUI");
    }

    public void showInventory(bool b)
    {
        inventoryUI.SetActive(b);
    }

}
