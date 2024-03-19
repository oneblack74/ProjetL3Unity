using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // Manager
    private GameManager manager;

    // Variable
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    void Update()
    {
        PlayerController playerController = manager.GetPlayerController;
        HealthControl healthControl = playerController.GetHealthControl;
        image.fillAmount = healthControl.GetHealth / healthControl.GetMaxHealth;
    }
}
