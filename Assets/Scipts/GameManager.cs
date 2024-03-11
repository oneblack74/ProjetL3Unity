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
    private SaveData saveData;

    [SerializeField] private GameObject prefabCursorUI;
    private GameObject cursorUI;

    private readonly Dictionary<int, ItemDefinition> itemDico = new();

    void Awake()
    {
        if (GetInstance() != null)
        {
            Destroy(gameObject);
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
        saveData = GetComponent<SaveData>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
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
        saveData.Sauvegarder();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            // ajouter ici du code pour afficher une barre de progression
            // Debug.Log("Progress: " + operation.progress);

            yield return null;
        }
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        saveData.Charger();
        if (GetSceneIndex == 2)
        {
            GameObject[] portails = GameObject.FindGameObjectsWithTag("Portail");
            int ind = Random.Range(0, portails.Length);
            PortailTP portail = portails[ind].GetComponent<PortailTP>();
            portail.GetState = PortailTP.State.Start;
            playerController.transform.position = portails[ind].transform.position;
        }
        else if (GetSceneIndex == 1)
        {
            PortailTP portails = GameObject.FindGameObjectWithTag("Portail").GetComponent<PortailTP>();
            portails.GetState = PortailTP.State.Start;
            playerController.transform.position = portails.transform.position;
        }
    }

    // S'occupe de changer en mode "dans un inventaire"
    public void OpenInventory()
    {
        playerController.LockPlayer(true);
        cursorUI = Instantiate(prefabCursorUI);
        Transform parent = GameObject.Find("UI").transform;
        cursorUI.transform.SetParent(parent, false);
        playerInInventory = true;
    }

    // S'occupe de changer en mode "hors d'un inventaire"
    public void CloseInventory()
    {
        playerController.LockPlayer(false);
        Destroy(cursorUI);
        cursorUI = null;
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
