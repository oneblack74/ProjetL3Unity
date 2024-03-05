using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    public PlayerInput inputs;

    private PlayerController playerController;
    [SerializeField] private GameObject cursorUI;

    private readonly Dictionary<int, ItemDefinition> itemDico = new();
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // récupérer les item scriptableobject et les stocker dans un dico
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
    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // S'occupe de changer en mode "dans un inventaire", TODO : Empécher les controlles
    public void OpenInventory()
	{
        cursorUI.SetActive(true);
	}

    // S'occupe de changer en mode "dans un inventaire" Cf : GameManager.OpenInventory()
    public void CloseInventory()
	{
        cursorUI.SetActive(false);
	}

    public int GetSceneIndex
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    public ItemDefinition ConvertIdToItem(int ID)
    {
        return itemDico[ID];
    }

    public PlayerInput GetInputs
    {
        get { return inputs; }
    }

    public PlayerController GetPlayerController
    {
        get { return playerController; }
    }

    public GameObject GetCursorUI 
    {
        get { return cursorUI; }
    }
}
