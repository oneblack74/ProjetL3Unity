using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerInput inputs;
    private AudioSource audioSource;

    private PlayerController playerController;
    private UIHotbar hotbar;
    private bool playerInInventory; // Variable qui gère si le player est dans un inventaire AUTRE que le siens 
    private SaveData saveData;

    [SerializeField] private GameObject prefabCursorUI;
    private GameObject cursorUI;

    private readonly Dictionary<int, ItemDefinition> itemDico = new();
    private readonly Dictionary<string, AudioClip> soundDico = new();
    [SerializeField] private GameObject PrefabMenuUI;
    private GameObject actualMenuUI;
    private bool inMenu;

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
        ItemDefinition[] items = Resources.LoadAll<ItemDefinition>("Items");

        foreach (ItemDefinition item in items)
        {
            if (item != null)
            {
                itemDico.Add(item.getID, item);
            }
        }

        // récupérer les sons et les stocker dans un dico
        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip sound in sounds)
        {
            if (sound != null)
            {
                soundDico.Add(sound.name, sound);
            }
        }

        saveData = GetComponent<SaveData>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        hotbar = GameObject.Find("UI/Hotbar/HotbarSelectUI").GetComponent<UIHotbar>();
    }



    public static GameManager GetInstance()
    {
        return Instance;
    }

    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(soundDico[soundName]);
    }

    public void QuitGame()
    {
        Application.Quit();
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
        hotbar = GameObject.Find("/UI/Hotbar/HotbarSelectUI").GetComponent<UIHotbar>();
        saveData.Charger();
        if (GetSceneIndex == 2)
        {
            GameObject[] portails = GameObject.FindGameObjectsWithTag("Portail");
            List<GameObject> portailsValide = new();
            foreach (GameObject p in portails)
            {
                if (p.GetComponent<PortailTP>().GetState != PortailTP.State.Desactiver)
                {
                    portailsValide.Add(p);
                }
            }
            int ind = Random.Range(0, portailsValide.Count);
            PortailTP portail = portailsValide[ind].GetComponent<PortailTP>();
            portail.GetState = PortailTP.State.Start;
            playerController.transform.position = portailsValide[ind].transform.position;
        }
        else if (GetSceneIndex == 1)
        {
            PortailTP portails = GameObject.FindGameObjectWithTag("Portail").GetComponent<PortailTP>();
            portails.GetState = PortailTP.State.Start;
            playerController.transform.position = portails.transform.position;
        }
    }

    public void OpenMenu()
    {
        inMenu = true;
        playerController.LockPlayer(true);
        actualMenuUI = Instantiate(PrefabMenuUI);
        Transform parent = GameObject.Find("UI").transform;
        actualMenuUI.transform.SetParent(parent, false);
        actualMenuUI.transform.SetAsLastSibling();
        actualMenuUI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public void CloseMenu()
    {
        inMenu = false;
        playerController.LockPlayer(false);
        Destroy(actualMenuUI);
    }

    // S'occupe de changer en mode "dans un inventaire"
    public void OpenInventory()
    {
        playerController.LockPlayer(true);
        if (hotbar != null)
            hotbar.gameObject.transform.parent.transform.GetChild(hotbar.gameObject.transform.parent.transform.childCount - 1).GetComponent<Image>().raycastTarget = false;
        cursorUI = Instantiate(prefabCursorUI);
        Transform parent = GameObject.Find("UI").transform;
        cursorUI.transform.SetParent(parent, false);
        playerInInventory = true;
    }

    // S'occupe de changer en mode "hors d'un inventaire"
    public void CloseInventory()
    {
        playerController.LockPlayer(false);
        if (hotbar != null)
            hotbar.gameObject.transform.parent.transform.GetChild(hotbar.gameObject.transform.parent.transform.childCount - 1).GetComponent<Image>().raycastTarget = true;
        Destroy(cursorUI);
        cursorUI = null;
        playerInInventory = false;
    }

    public void MoveSelect(int way)
    {
        if (!inMenu)
        {
            PlaySound("Scroll");
            hotbar.MoveSelect(way);
        }
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

    public bool GetInMenu
    {
        get { return inMenu; }
    }

}
