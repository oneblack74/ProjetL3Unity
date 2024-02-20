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
        if (Input.GetKeyDown(KeyCode.W))
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
        Debug.Log(chemin);

        System.IO.File.WriteAllText(chemin, saveData);
    }

    public void Charger()
    {
        string chemin = Application.persistentDataPath + "/SaveData.json";
        string saveData = System.IO.File.ReadAllText(chemin);
        Debug.Log("Sauvegarder");
        data = JsonUtility.FromJson<Data>(saveData);
        LoadElements();
    }

    private void SaveElements()
    {
        SaveInventory(manager.GetPlayerController.GetInventory);
    }

    private void SaveInventory(Inventory inventory)
    {
        data.slots = new List<Slot>();
        foreach (Slot slot in inventory.GetTab)
        {
            data.slots.Add(slot);
        }
        foreach (Slot slot in data.slots)
        {
            Debug.Log(slot.GetItemQuantity);
        }
    }

    private void LoadInventory(Inventory inventory)
    {
        inventory.RedefineSlots(data.slots);
    }

    private void LoadElements()
    {
        LoadInventory(manager.GetPlayerController.GetInventory);
    }
}

[System.Serializable]
public class Data
{
    [SerializeField] public List<Slot> slots;
}
