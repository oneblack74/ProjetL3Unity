using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaBar : MonoBehaviour
{
    private GameManager manager;
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
        StaminaControl staminaControl = playerController.GetStaminaControl;
        image.fillAmount = staminaControl.GetStamina / staminaControl.GetMaxStamina;
    }
}
