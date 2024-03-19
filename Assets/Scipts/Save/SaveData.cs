using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Data data = new();
    private GameManager manager;
    void Start()
    {
        manager = GameManager.GetInstance();
    }

    public void Sauvegarder()
    {
        Data? oldData = LoadJson() ?? new Data();
        SaveElements((Data)oldData);

        string saveData = JsonUtility.ToJson(data);
        string chemin = Application.persistentDataPath + "/SaveData.json";

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
        // __________ hotbar
        data.player.hotbar = SaveInventory(GameObject.Find("UI/Hotbar/HotbarInventoryUI").GetComponent<Inventory>());

        // portails : ==========================================
        data.portails = SavePortails(oldData);

        // coffres : ===========================================
        data.coffres = SaveCoffres(oldData);

        // coord : =============================================
        if (manager.GetSceneIndex == 1)
            data.coord = SaveCoord();

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
        // __________ hotbar
        LoadInventory(GameObject.Find("UI/Hotbar/HotbarInventoryUI").GetComponent<Inventory>(), data.player.hotbar);

        // portails : ==========================================
        LoadPortails(data.portails);

        // coffres : ===========================================
        LoadCoffres(data.coffres);

        // coord : =============================================
        if (manager.GetSceneIndex == 1)
            LoadCoord(data.coord);
    }


    private List<StructSlot> SaveInventory(Inventory inventory)
    {
        List<StructSlot> slots = new();
        foreach (Slot slot in inventory.GetTab)
        {
            StructSlot s = new()
            {
                itemID = slot.GetItem.GetID,
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
        List<StructPortails> portails = new();
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

    private List<StructCoffres> SaveCoffres(Data oldData)
    {
        List<StructCoffres> coffres = new();
        GameObject[] gameObjectsCoffre = GameObject.FindGameObjectsWithTag("Chest");

        for (int i = 0; i < gameObjectsCoffre.Length; i++)
        {
            Chest coffre = gameObjectsCoffre[i].GetComponent<Chest>();
            StructCoffres c = new()
            {
                id = coffre.GetID,
                inventory = SaveInventory(coffre.GetComponent<Inventory>())
            };
            coffres.Add(c);
        }
        if (oldData.coffres != null)
        {
            for (int i = 0; i < oldData.coffres.Count; i++)
            {
                bool exist = false;
                for (int j = 0; j < gameObjectsCoffre.Length; j++)
                {
                    Chest coffre = gameObjectsCoffre[j].GetComponent<Chest>();
                    if (coffre.GetID == oldData.coffres[i].id)
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    StructCoffres c = new()
                    {
                        id = oldData.coffres[i].id,
                        inventory = oldData.coffres[i].inventory
                    };
                    coffres.Add(c);
                }
            }
        }
        return coffres;
    }

    private List<StructCoord> SaveCoord()
    {
        List<StructCoord> coord = new();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Ressource");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            StructCoord c = new()
            {
                x = gameObjects[i].transform.position.x,
                y = gameObjects[i].transform.position.y
            };
            coord.Add(c);
        }
        return coord;
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

    private void LoadCoffres(List<StructCoffres> coffres)
    {
        GameObject[] gameObjectsCoffre = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < gameObjectsCoffre.Length; i++)
        {
            Chest coffre = gameObjectsCoffre[i].GetComponent<Chest>();
            for (int j = 0; j < coffres.Count; j++)
            {
                if (coffre.GetID == coffres[j].id)
                {
                    LoadInventory(coffre.GetComponent<Inventory>(), coffres[j].inventory);
                }
            }
        }
    }

    private void LoadCoord(List<StructCoord> coord)
    {
        for (int i = 0; i < coord.Count; i++)
        {
            GameObject ressource = Instantiate(manager.GetPlayerController.GetPrefabRessource);
            ressource.transform.position = new Vector3(coord[i].x, coord[i].y, 0);
        }
    }

}

[System.Serializable]
public struct Data
{
    public List<StructCoffres> coffres;
    public List<StructPortails> portails;
    public List<StructCoord> coord;
    public StructPlayer player;
}

[System.Serializable]
public class StructCoord
{
    public float x;
    public float y;
}

[System.Serializable]
public class StructCoffres
{
    public int id;
    public List<StructSlot> inventory;
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
    public List<StructSlot> hotbar;
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
