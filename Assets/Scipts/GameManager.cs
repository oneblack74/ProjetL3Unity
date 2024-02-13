using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }
    public PlayerInput inputs;
    public GameObject player; // A SUPPRIMER, PÖUR TEST
    private PlayerController playerController; // A SUPPRIMER, POUR TEST
    private Dictionary<int, ItemDefinition> itemDico = new Dictionary<int, ItemDefinition>();
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
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
        playerController = player.GetComponent<PlayerController>();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public int GetSceneIndex
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    public ItemDefinition ConvertIdToItem(int ID)
    {
        return itemDico[ID];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TestChangeScene();
        if (Input.GetKeyDown(KeyCode.I)) playerController.ShowInventory(true);
        if (Input.GetKeyDown(KeyCode.O)) playerController.ShowInventory(false);
        if (Input.GetKeyDown(KeyCode.J)) player.GetComponent<Inventory>().AddItem(0, ConvertIdToItem(1), 1);
    }

    public void TestChangeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ChangeScene(2);
        }
        else
        {
            ChangeScene(1);
        }
    }

    public PlayerInput GetInputs
    {
        get { return inputs; }
    }
}
