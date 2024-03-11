using UnityEngine;


public class HealthControl : MonoBehaviour
{
    [SerializeField] private float maxHealth = 0f;          //? Maximum health
    [SerializeField] private float health = 0f;              //? Actual health


    public void AddHealth(float value)
    {
        if (value + health > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += value;
        }
    }

    public void RemoveHealth(float value)
    {
        if (health - value < 0)
        {
            health = 0;
        }
        else
        {
            health -= value;
        }
    }

    public void InitValues(float maxHealth, float health)
    {
        this.maxHealth = maxHealth;
        this.health = health;
    }

    public float GetHealth
    {
        get { return health; }
    }

    public float GetMaxHealth
    {
        get { return maxHealth; }
    }
}