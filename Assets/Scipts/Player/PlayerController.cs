using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cursorUI;

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


    void Awake()
    {
        inventoryUI.SetActive(false);
        cursorUI.SetActive(false);

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
        GameManager.GetInstance().GetInputs.actions["Dash"].performed += context => dash.ActiveDash();
        sprintAction = GameManager.GetInstance().GetInputs.actions["Sprint"];
        moveAction = GameManager.GetInstance().GetInputs.actions["Move"];
    }

    public void ShowInventory(InputAction.CallbackContext context)
    {
        inventoryVisibility = !inventoryVisibility;
        inventoryUI.SetActive(inventoryVisibility);
        cursorUI.SetActive(inventoryVisibility);
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
