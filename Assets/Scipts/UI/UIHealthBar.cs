using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
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
        HealthControl healthControl = manager.GetPlayerController.GetHealthControl;
        image.fillAmount = healthControl.GetHealth / healthControl.GetMaxHealth;
    }
}
