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
    private bool inventoryVisibility;
    private Vector3 moveValue;
    private bool isSprinting;

    private List<IInteractable> triggerList = new();

    void Awake()
    {
        inventoryUI.SetActive(false);
        GameManager.GetInstance().CloseInventory();

        inventory = GetComponent<Inventory>();
        staminaControl = GetComponent<StaminaControl>();
        healthControl = GetComponent<HealthControl>();
        movement = GetComponent<Movement>();
        sprint = GetComponent<Sprint>();
        dash = GetComponent<Dash>();
    }

    void Start()
    {
        GameManager.GetInstance().GetInputs.actions["OpenInventory"].performed += ShowInventory;
        GameManager.GetInstance().GetInputs.actions["Interact"].performed += Interact;
        GameManager.GetInstance().GetInputs.actions["Dash"].performed += context => dash.ActiveDash();
        sprintAction = GameManager.GetInstance().GetInputs.actions["Sprint"];
        moveAction = GameManager.GetInstance().GetInputs.actions["Move"];
        inventory.AddItemFast(GameManager.GetInstance().ConvertIdToItem(1), 5);
    }

    public void ShowInventory(InputAction.CallbackContext context)
    {
        inventoryVisibility = !inventoryVisibility;
        inventoryUI.SetActive(inventoryVisibility);
        GameManager.GetInstance().OpenInventory();
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
        if (triggerList.Count == 0)
		{
            return;
		}
        triggerList[0].Interact();
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
