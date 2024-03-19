using UnityEngine;


public class StaminaControl : MonoBehaviour
{
    // SerilizedField
    [SerializeField] private float maxStamina = 0f;
    [SerializeField] private float stamina = 0f;
    [SerializeField] private float regenDelay = 0f;
    [SerializeField] private float refillRate = 0f;

    // Manager
    private GameManager manager;

    // Timer
    private float timer = 0f;

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public bool DrainStamina(float s)
    {
        timer = 0f;
        if (stamina >= s)
        {
            stamina -= s; return true;
        }
        else
            return false;

    }

    public void DrainStaminaOverTime(float rate)
    {
        timer = 0f;
        stamina -= rate * Time.deltaTime;
    }

    public void RegenStamina()
    {
        if (this.timer >= regenDelay)
        {
            if (stamina < maxStamina)
            {
                stamina += refillRate * Time.deltaTime;
                if (stamina > maxStamina)
                    stamina = maxStamina;
            }
        }
    }

    private void UpdateTimer()
    {
        if (timer < regenDelay)
        {
            timer += Time.deltaTime;
        }
    }

    void Update()
    {
        if (!manager.GetInMenu)
        {
            UpdateTimer();
            RegenStamina();
        }
    }

    public void InitValues(float maxStamina, float stamina, float regenDelay, float refillRate)
    {
        this.maxStamina = maxStamina;
        this.stamina = stamina;
        this.regenDelay = regenDelay;
        this.refillRate = refillRate;
    }

    public float GetStamina
    {
        get { return stamina; }
    }

    public float GetMaxStamina
    {
        get { return maxStamina; }
    }

    public float GetRegenDelay
    {
        get { return regenDelay; }
    }

    public float GetRefillRate
    {
        get { return refillRate; }
    }
}