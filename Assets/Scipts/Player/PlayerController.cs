using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject prefabInventoryUI;
    private GameObject inventoryUI;

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

    void OnDestroy()
    {
        if (manager != null)
        {
            manager.GetInputs.actions["OpenInventory"].performed -= ShowInventory;
            manager.GetInputs.actions["Interact"].performed -= Interact;
            manager.GetInputs.actions["Dash"].performed -= dash.ActiveDash;
            manager.GetInputs.actions["OpenMenu"].performed -= ShowMenu;
        }
    }
}
