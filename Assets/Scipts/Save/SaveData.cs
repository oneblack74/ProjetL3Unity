using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Data data = new Data();
    private GameManager manager;
    void Start()
    {
        manager = GameManager.GetInstance();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Charger();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Sauvegarder();
        }
    }
    public void Sauvegarder()
    {
        SaveElements();

        string saveData = JsonUtility.ToJson(data);
        string chemin = Application.persistentDataPath + "/SaveData.json";
        Debug.Log(saveData);
        Debug.Log(chemin);

        System.IO.File.WriteAllText(chemin, saveData);
    }

    public void Charger()
    {
        string chemin = Application.persistentDataPath + "/SaveData.json";
        string saveData = System.IO.File.ReadAllText(chemin);
        Debug.Log("Charger");
        data = JsonUtility.FromJson<Data>(saveData);
        LoadElements();
    }

    private void SaveElements()
    {
        // player : ============================================
        // __________ inventory
        data.player.slots = SaveInventory(manager.GetPlayerController.GetInventory);
        // __________ health
        data.player.health = SaveHealth(manager.GetPlayerController.GetHealthControl);
        // __________ stamina
        data.player.stamina = SaveStamina(manager.GetPlayerController.GetStaminaControl);
    }
    private void LoadElements()
    {
        // player : ============================================
        // __________ inventory
        LoadInventory(manager.GetPlayerController.GetInventory, data.player.slots);
        // __________ health
        LoadHealth(manager.GetPlayerController.GetHealthControl, data.player.health);
        // __________ stamina
        LoadStamina(manager.GetPlayerController.GetStaminaControl, data.player.stamina);
    }

    private List<StructSlot> SaveInventory(Inventory inventory)
    {
        List<StructSlot> slots = new List<StructSlot>();
        foreach (Slot slot in inventory.GetTab)
        {
            StructSlot s = new()
            {
                itemID = slot.GetItem.getID,
                itemQuantity = slot.GetItemQuantity
            };
            slots.Add(s);
        }
        return slots;
    }

    private StructHealth SaveHealth(HealthControl healthControl)
    {
        StructHealth health = new()
        {
            maxHealth = healthControl.GetMaxHealth,
            health = healthControl.GetHealth
        };
        return health;
    }

    private StructStamina SaveStamina(StaminaControl staminaControl)
    {
        StructStamina stamina = new()
        {
            maxStamina = staminaControl.GetMaxStamina,
            stamina = staminaControl.GetStamina,
            regenDelay = staminaControl.GetRegenDelay,
            refillRate = staminaControl.GetRefillRate
        };
        return stamina;
    }

    private void LoadInventory(Inventory inventory, List<StructSlot> slots)
    {
        inventory.RedefineSlots(slots);
    }

    private void LoadHealth(HealthControl healthControl, StructHealth health)
    {
        healthControl.InitValues(health.maxHealth, health.health);
    }

    private void LoadStamina(StaminaControl staminaControl, StructStamina stamina)
    {
        staminaControl.InitValues(stamina.maxStamina, stamina.stamina, stamina.regenDelay, stamina.refillRate);
    }

}

[System.Serializable]
public struct Data
{
    public StructPlayer player;
}

[System.Serializable]
public class StructPlayer
{
    public StructHealth health;
    public StructStamina stamina;
    public List<StructSlot> slots;
}

[System.Serializable]
public class StructSlot
{
    public int itemID;
    public int itemQuantity;
}

[System.Serializable]
public class StructHealth
{
    public float maxHealth;
    public float health;
}

[System.Serializable]
public class StructStamina
{
    public float maxStamina;
    public float stamina;
    public float regenDelay;
    public float refillRate;
}
