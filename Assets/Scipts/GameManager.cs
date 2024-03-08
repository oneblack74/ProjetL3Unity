using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerInput inputs;

    private PlayerController playerController;
    private bool playerInInventory; // Variable qui gère si le player est dans un inventaire AUTRE que le siens 
    private GameObject cursorUI;

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





    public static GameManager GetInstance()
    {
        return Instance;
    }

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneIndex));
    }

    public IEnumerator ChangeSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            // ajouter ici du code pour afficher une barre de progression
            // Debug.Log("Progress: " + operation.progress);

            yield return null;
        }
        cursorUI = GameObject.Find("CursorUI");
    }


    // S'occupe de changer en mode "dans un inventaire"
    public void OpenInventory()
    {
        playerController.LockPlayer(true);
        cursorUI.SetActive(true);
        playerInInventory = true;
    }

    // S'occupe de changer en mode "dans un inventaire"
    public void CloseInventory()
    {
        playerController.LockPlayer(false);
        if (cursorUI == null) cursorUI = GameObject.Find("CursorUI");
        cursorUI.SetActive(false);
        playerInInventory = false;
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
        set { playerController = value; }
    }

    public GameObject GetCursorUI
    {
        get { return cursorUI; }
    }

    public bool GetIsPlayerInInventory
    {
        get { return playerInInventory; }
    }
}
