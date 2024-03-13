using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEditor;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject prefabInventoryUI;
    private GameObject inventoryUI;
    [SerializeField] private GameObject prefabRessource;

    // Components
    private Inventory inventory;
    private StaminaControl staminaControl;
    private HealthControl healthControl;
    private Movement movement;
    private Sprint sprint;
    private Dash dash;

    // Actions
    private InputAction sprintAction;
    private InputAction moveAction;
    private InputAction scrollAction;

    // Variables
    private bool inInventory = false; // Variable qui g�re si le joueur est dans SON inventaire
    private Vector3 moveValue;
    private bool isSprinting;
    private Vector2 scrollValue;

    private readonly List<IInteractable> triggerList = new();
    private GameManager manager;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        staminaControl = GetComponent<StaminaControl>();
        healthControl = GetComponent<HealthControl>();
        movement = GetComponent<Movement>();
        sprint = GetComponent<Sprint>();
        dash = GetComponent<Dash>();
    }

    void Start()
    {
        manager = GameManager.GetInstance();
        manager.GetInputs.actions["OpenInventory"].performed += ShowInventory;
        manager.GetInputs.actions["Interact"].performed += Interact;
        manager.GetInputs.actions["Dash"].performed += dash.ActiveDash;
        manager.GetInputs.actions["OpenMenu"].performed += ShowMenu;
        manager.GetInputs.actions["PlaceBlock"].performed += PlaceBlock;
        sprintAction = manager.GetInputs.actions["Sprint"];
        moveAction = manager.GetInputs.actions["Move"];
        scrollAction = manager.GetInputs.actions["SwitchSelect"];
        manager.CloseInventory();
    }

    public void ShowMenu(InputAction.CallbackContext context)
    {
        if (manager.GetInMenu)
        {
            manager.CloseMenu();
        }
        else
        {
            manager.OpenMenu();
        }
    }

    public void ShowInventory(InputAction.CallbackContext context)
    {
        if (manager.GetIsPlayerInInventory && !inInventory && !manager.GetInMenu)
        {
            return;
        }
        if (inInventory)
        {
            Destroy(inventoryUI);
            inventoryUI = null;
            inInventory = false;
            manager.CloseInventory();
        }
        else
        {
            inventoryUI = Instantiate(prefabInventoryUI);
            Transform parent = GameObject.Find("UI").transform;
            inventoryUI.transform.SetParent(parent, false);
            inventoryUI.transform.SetSiblingIndex(0);
            inventoryUI.transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(inventory);
            inInventory = true;
            manager.OpenInventory();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (triggerList.Count == 0 || inInventory)
        {
            return;
        }
        triggerList[0].Interact(); //TODO: A modifier pour que ce soit l'objet le plus proche  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Interactable>() == null)
        {
            return;
        }
        IInteractable objectInteracted = collision.gameObject.GetComponent<IInteractable>();
        if (!triggerList.Contains(objectInteracted))
        {
            triggerList.Add(objectInteracted);
            collision.gameObject.GetComponent<Interactable>().ChangeNear();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Interactable>() == null)
        {
            return;
        }
        IInteractable objectInteracted = collision.gameObject.GetComponent<IInteractable>();
        if (triggerList.Contains(objectInteracted))
        {
            triggerList.Remove(objectInteracted);
            collision.gameObject.GetComponent<Interactable>().ChangeNear();
        }
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        isSprinting = sprintAction.ReadValue<float>() > 0;
        scrollValue = scrollAction.ReadValue<Vector2>();
        if (scrollValue.y > 100)
        {
            manager.MoveSelect(-1);
        }
        if (scrollValue.y < -100)
        {
            manager.MoveSelect(1);
        }
    }

    public void PlaceBlock(InputAction.CallbackContext context)
    {
        if (!manager.GetIsPlayerInInventory && !manager.GetInMenu && manager.GetSceneIndex == 1)
        {
            int slot = manager.GetHotbar.GetSlot;
            int itemID = manager.GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().CheckItem(slot).getID;
            if (itemID == 0)
            {
                return;
            }
            Vector2 mousePos = Mouse.current.position.ReadValue();
            if (Physics2D.OverlapArea(new Vector2(mousePos.x - 0.5f, mousePos.y - 0.5f), new Vector2(mousePos.x + 0.5f, mousePos.y + 0.5f)) != null)
            {
                return; // TODO: ça marche pas
            }
            GameObject res = Instantiate(prefabRessource);
            res.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            res.GetComponent<Ressource>().GetitemID = itemID;
            manager.GetHotbar.transform.parent.GetChild(0).GetComponent<Inventory>().RemoveItem(slot, 1);
            res.GetComponent<Ressource>().GetQuantity = 1;
            res.GetComponent<SpriteRenderer>().sprite = GameManager.GetInstance().ConvertIdToItem(itemID).getIcon;
            res.GetComponent<Interactable>().GetDefaultSprite = GameManager.GetInstance().ConvertIdToItem(itemID).getIcon;
            res.GetComponent<Interactable>().GetNearSprite = GameManager.GetInstance().ConvertIdToItem(itemID).getNearIcon;
        }
    }

    void FixedUpdate()
    {
        sprint.Sprinting(isSprinting, staminaControl, movement);
        if (!movement.GetIsLock)
            movement.Move(moveValue);
    }

    public void LockPlayer(bool state)
    {
        movement.GetIsLock = state;
    }

    public Inventory GetInventory
    {
        get { return inventory; }
    }

    public HealthControl GetHealthControl
    {
        get { return healthControl; }
    }

    public StaminaControl GetStaminaControl
    {
        get { return staminaControl; }
    }

    public Movement GetMovement
    {
        get { return movement; }
    }

    public GameObject GetPrefabRessource
    {
        get { return prefabRessource; }
    }

    void OnDestroy()
    {
        if (manager != null)
        {
            manager.GetInputs.actions["OpenInventory"].performed -= ShowInventory;
            manager.GetInputs.actions["Interact"].performed -= Interact;
            manager.GetInputs.actions["Dash"].performed -= dash.ActiveDash;
            manager.GetInputs.actions["OpenMenu"].performed -= ShowMenu;
            manager.GetInputs.actions["PlaceBlock"].performed -= PlaceBlock;
        }
    }
}
