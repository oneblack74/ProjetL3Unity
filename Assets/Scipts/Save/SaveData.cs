using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Data data = new Data();
    private GameManager manager;
    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public void Sauvegarder()
    {
        Data? oldData = LoadJson();
        if (oldData == null)
        {
            oldData = new Data();
        }
        SaveElements((Data)oldData);

        string saveData = JsonUtility.ToJson(data);
        string chemin = Application.persistentDataPath + "/SaveData.json";
        //Debug.Log(saveData);
        //Debug.Log(chemin);

        System.IO.File.WriteAllText(chemin, saveData);
    }

    private Data? LoadJson()
    {
        string chemin = Application.persistentDataPath + "/SaveData.json";
        if (!System.IO.File.Exists(chemin))
        {
            return null;
        }
        string saveData = System.IO.File.ReadAllText(chemin);
        return JsonUtility.FromJson<Data>(saveData);
    }

    public void Charger()
    {
        data = (Data)LoadJson();
        LoadElements();
    }

    private void SaveElements(Data oldData)
    {
        // player : ============================================
        // __________ inventory
        data.player.slots = SaveInventory(manager.GetPlayerController.GetInventory);
        // __________ health
        data.player.health = SaveHealth(manager.GetPlayerController.GetHealthControl);
        // __________ stamina
        data.player.stamina = SaveStamina(manager.GetPlayerController.GetStaminaControl);

        // portails : ==========================================
        data.portails = SavePortails(oldData);
        Debug.Log(data.portails);
        Debug.Log(data.portails.Count);
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

        // portails : ==========================================
        LoadPortails(data.portails);
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

    private List<StructPortails> SavePortails(Data oldData)
    {
        List<StructPortails> portails = new List<StructPortails>();
        GameObject[] gameObjectsPortail = GameObject.FindGameObjectsWithTag("Portail");

        for (int i = 0; i < gameObjectsPortail.Length; i++)
        {
            PortailTP portail = gameObjectsPortail[i].GetComponent<PortailTP>();
            bool desactiver = false;
            if (portail.GetState == PortailTP.State.Desactiver)
            {
                desactiver = true;
            }
            else
            {
                if (oldData.portails != null)
                {
                    for (int j = 0; j < oldData.portails.Count; j++)
                    {
                        if (portail.GetID == oldData.portails[j].id)
                        {
                            if (oldData.portails[j].desactiver) desactiver = true;
                        }
                    }
                }
            }
            StructPortails p = new()
            {
                id = portail.GetID,
                desactiver = desactiver
            };
            portails.Add(p);
        }
        if (oldData.portails != null)
        {
            for (int i = 0; i < oldData.portails.Count; i++)
            {
                bool exist = false;
                for (int j = 0; j < gameObjectsPortail.Length; j++)
                {
                    PortailTP portail = gameObjectsPortail[j].GetComponent<PortailTP>();
                    if (portail.GetID == oldData.portails[i].id)
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    StructPortails p = new()
                    {
                        id = oldData.portails[i].id,
                        desactiver = oldData.portails[i].desactiver
                    };
                    portails.Add(p);
                }
            }
        }
        return portails;
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

    private void LoadPortails(List<StructPortails> portails)
    {
        GameObject[] gameObjectsPortail = GameObject.FindGameObjectsWithTag("Portail");
        for (int i = 0; i < gameObjectsPortail.Length; i++)
        {
            PortailTP portail = gameObjectsPortail[i].GetComponent<PortailTP>();
            for (int j = 0; j < portails.Count; j++)
            {
                if (portail.GetID == portails[j].id)
                {
                    if (portails[j].desactiver)
                    {
                        portail.GetState = PortailTP.State.Desactiver;
                        portail.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
    }

}

[System.Serializable]
public struct Data
{
    public List<StructPortails> portails;
    public StructPlayer player;
}

[System.Serializable]
public class StructPortails
{
    public int id;
    public bool desactiver;
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
