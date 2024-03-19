using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
public class Cursor : MonoBehaviour
{
    [SerializeField] private SlotUI slotUI;
    private bool following = true;
    private Inventory inventory;
    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetInstance();
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
        manager.GetPlayerController.GetInventory.AddItemFast(inventory.CheckItem(0), inventory.CheckItemQuantity(0));
    }
}
