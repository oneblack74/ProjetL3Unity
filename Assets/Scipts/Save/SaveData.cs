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
        Debug.Log(data);
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
        SaveInventory(manager.GetPlayerController.GetInventory);

        // __________ health
        SaveHealth(manager.GetPlayerController.GetHealthControl);
    }
    private void LoadElements()
    {
        // player : ============================================
        // __________ inventory
        LoadInventory(manager.GetPlayerController.GetInventory);
        // __________ health
        LoadHealth(manager.GetPlayerController.GetHealthControl);
    }

    private void SaveInventory(Inventory inventory)
    {
        data.slots = new List<StructSlot>();
        foreach (Slot slot in inventory.GetTab)
        {
            StructSlot s = new()
            {
                itemID = slot.GetItem.getID,
                itemQuantity = slot.GetItemQuantity
            };
            data.slots.Add(s);
        }

    }

    private void SaveHealth(HealthControl healthControl)
    {
        data.health = new()
        {
            maxHealth = healthControl.GetMaxHealth,
            health = healthControl.GetHealth
        };
    }

    private void LoadInventory(Inventory inventory)
    {
        inventory.RedefineSlots(data.slots);
    }

    private void LoadHealth(HealthControl healthControl)
    {
        healthControl.InitValues(data.health.maxHealth, data.health.health);
    }

}

[System.Serializable]
public struct Data
{
    public List<StructSlot> slots;
    public StructHealth health;
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
