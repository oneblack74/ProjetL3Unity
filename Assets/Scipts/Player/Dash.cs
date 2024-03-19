using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(StaminaControl))]
[RequireComponent(typeof(BoxCollider2D))]
public class Dash : MonoBehaviour
{
    // Components
    private Movement movement;
    private StaminaControl staminaControl;
    private BoxCollider2D boxCollider;

    // SerilizedField
    [SerializeField] private float timerMax;
    [SerializeField] private float distance;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float staminaCount = 10.0f;

    // Variables
    private float speedDash;
    private float timer;
    private Movement.Dir dashDir;

    // Manager
    private GameManager manager;

    void Awake()
    {
        movement = GetComponent<Movement>();
        staminaControl = GetComponent<StaminaControl>();
        boxCollider = GetComponent<BoxCollider2D>();
        CalculSpeedMultiplier();
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    private void CalculSpeedMultiplier()
    {
        speedDash = distance / timerMax;
    }

    private bool TestDash()
    {
        Vector2 dir = Vector2.zero;
        switch (dashDir)
        {
            case Movement.Dir.Up:
                dir = Vector2.up;
                break;
            case Movement.Dir.Down:
                dir = Vector2.down;
                break;
            case Movement.Dir.Left:
                dir = Vector2.left;
                break;
            case Movement.Dir.Right:
                dir = Vector2.right;
                break;
            default:
                break;
        }

        Vector2 endPoint = (Vector2)transform.position + dir * distance;
        Collider2D hit = Physics2D.OverlapCircle(endPoint, 0.1f);

        Debug.DrawLine(transform.position, endPoint, Color.red, 1);
        return hit == null;
    }

    public void ActiveDash(InputAction.CallbackContext context)
    {
        if (!isDashing && staminaControl.GetStamina >= staminaCount)
        {
            CalculSpeedMultiplier();
            manager.PlaySound("Dash");
            staminaControl.DrainStamina(staminaCount);
            isDashing = true;
            dashDir = movement.GetMyDir;
            if (TestDash())
            {
                boxCollider.enabled = false;
            }
            timer = 0;
            movement.GetSpeed = speedDash;
        }
    }

    void Update()
    {
        if (isDashing)
        {
            if (timer < timerMax)
            {
                timer += Time.deltaTime;
            }
            else
            {
                movement.GetSpeed = movement.GetSpeedTmp;
                isDashing = false;
                boxCollider.enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            Vector3 moveVector = Vector3.zero;
            switch (dashDir)
            {
                case Movement.Dir.Up:
                    moveVector = new Vector3(0, 1, 0);
                    break;
                case Movement.Dir.Down:
                    moveVector = new Vector3(0, -1, 0);
                    break;
                case Movement.Dir.Left:
                    moveVector = new Vector3(-1, 0, 0);
                    break;
                case Movement.Dir.Right:
                    moveVector = new Vector3(1, 0, 0);
                    break;
                default:
                    break;
            }
            movement.Move(moveVector);
        }
    }
}
