using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;

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

    // Variables
    private bool inInventory = false; // Variable qui g�re si le joueur est dans SON inventaire
    private Vector3 moveValue;
    private bool isSprinting;

    private readonly List<IInteractable> triggerList = new();

    void Awake()
    {
        inventoryUI = GameObject.Find("InventoryUI");
        Debug.Log(inventoryUI);
        inventoryUI.SetActive(false);

        inventory = GetComponent<Inventory>();
        staminaControl = GetComponent<StaminaControl>();
        healthControl = GetComponent<HealthControl>();
        movement = GetComponent<Movement>();
        sprint = GetComponent<Sprint>();
        dash = GetComponent<Dash>();
    }

    void Start()
    {
        GameManager.GetInstance().GetPlayerController = this;
        GameManager.GetInstance().CloseInventory();

        GameManager.GetInstance().GetInputs.actions["OpenInventory"].performed += ShowInventory;
        GameManager.GetInstance().GetInputs.actions["Interact"].performed += Interact;
        GameManager.GetInstance().GetInputs.actions["Dash"].performed += context => dash.ActiveDash();
        sprintAction = GameManager.GetInstance().GetInputs.actions["Sprint"];
        moveAction = GameManager.GetInstance().GetInputs.actions["Move"];


    }

    public void ShowInventory(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().GetIsPlayerInInventory && !inInventory)
        {
            return;
        }
        if (inInventory)
        {
            inventoryUI.SetActive(false);
            GameManager.GetInstance().CloseInventory();
        }
        else
        {

            inventoryUI.SetActive(true);
            GameManager.GetInstance().OpenInventory();
            inventory.AddItemFast(GameManager.GetInstance().ConvertIdToItem(1), 5);
        }
        inInventory = !inInventory;
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

    public void Interact(InputAction.CallbackContext context)
    {
        if (triggerList.Count == 0 || inInventory)
        {
            return;
        }
        triggerList[0].Interact(); //TODO: A modifier pour que ce soit l'objet le plus proche  
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        isSprinting = sprintAction.ReadValue<float>() > 0;
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
}
