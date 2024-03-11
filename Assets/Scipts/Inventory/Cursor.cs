using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
public class Cursor : MonoBehaviour
{
    private bool following = true;
    [SerializeField] private SlotUI slotUI;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        inventory.InitSlots();
    }

    void Update()
    {
        slotUI.UpdateUI(inventory.CheckItem(0), inventory.CheckItemQuantity(0));
        if (following)
        {
            FollowMouse();
        }
    }

    public void SetFollowMouse(bool b)
    {
        following = b;
    }

    public void FollowMouse()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    private void OnDestroy()
    {
        GameManager.GetInstance().GetPlayerController.GetInventory.AddItemFast(inventory.CheckItem(0), inventory.CheckItemQuantity(0));
    }
}
