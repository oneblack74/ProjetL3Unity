using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cursorUI;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        inventoryUI.SetActive(false);
    }

    public void ShowInventory(bool b)
    {
        cursorUI.SetActive(b);
        inventoryUI.SetActive(b);
    }

    public Inventory GetInventory
    {
        get { return inventory; }
    }

}
