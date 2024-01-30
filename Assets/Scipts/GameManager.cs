using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Search;
using System.Text.RegularExpressions;

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
    }

    void Start()
    {
        // A modifier https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html
        DirectoryInfo items = new DirectoryInfo("./Assets/ScriptableObjects/Items/");
        FileInfo[] filesItems = items.GetFiles();
        foreach (FileInfo file in filesItems)
        {
            Regex noMeta = new Regex("*.meta");
            if (noMeta.IsMatch(file.FullName))
            {
                continue;
            }
            Debug.Log(file);
        }
    }

    public ItemDefinition ConvertIdToItem(int ID)
    {
        return itemDico[ID];
    }
}
