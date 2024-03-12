using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
        GameManager.GetInstance().CloseInventory();
        GameManager.GetInstance().GetInputs.actions["OpenInventory"].performed += ShowInventory;
        GameManager.GetInstance().GetInputs.actions["Interact"].performed += Interact;
        GameManager.GetInstance().GetInputs.actions["Dash"].performed += dash.ActiveDash;
        sprintAction = GameManager.GetInstance().GetInputs.actions["Sprint"];
        moveAction = GameManager.GetInstance().GetInputs.actions["Move"];
        scrollAction = GameManager.GetInstance().GetInputs.actions["SwitchSelect"];
    }

    public void ShowInventory(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().GetIsPlayerInInventory && !inInventory)
        {
            return;
        }
        if (inInventory)
        {
            Destroy(inventoryUI);
            inventoryUI = null;
            inInventory = false;
            GameManager.GetInstance().CloseInventory();
        }
        else
        {
            inventoryUI = Instantiate(prefabInventoryUI);
            Transform parent = GameObject.Find("UI").transform;
            inventoryUI.transform.SetParent(parent, false);
            inventoryUI.transform.SetSiblingIndex(0);
            inventoryUI.transform.GetChild(0).GetComponent<InventoryUI>().LinkInventory(inventory);
            inInventory = true;
            GameManager.GetInstance().OpenInventory();
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
        }
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        isSprinting = sprintAction.ReadValue<float>() > 0;
        scrollValue = scrollAction.ReadValue<Vector2>();
        if (scrollValue.y > 100)
        {
            GameManager.GetInstance().MoveSelect(-1);
        }
        if (scrollValue.y < -100)
        {
            GameManager.GetInstance().MoveSelect(1);
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
        if (GameManager.GetInstance() != null)
        {
            GameManager.GetInstance().GetInputs.actions["OpenInventory"].performed -= ShowInventory;
            GameManager.GetInstance().GetInputs.actions["Interact"].performed -= Interact;
            GameManager.GetInstance().GetInputs.actions["Dash"].performed -= dash.ActiveDash;
        }
    }
}
