using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Search;
using System.Text.RegularExpressions;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }
    private Dictionary<int, ItemDefinition> itemDico = new Dictionary<int, ItemDefinition>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        string[] guids = AssetDatabase.FindAssets("t:ItemDefinition", new[] { "Assets/ScriptableObjects/Items" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemDefinition item = AssetDatabase.LoadAssetAtPath<ItemDefinition>(path);
            if (item != null)
            {
                itemDico.Add(item.getID, item);
            }
        }

        foreach (int dico in itemDico.Keys)
        {
            Debug.Log(dico);
        }

    }


    public ItemDefinition ConvertIdToItem(int ID)
    {
        return itemDico[ID];
    }
}
