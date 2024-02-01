using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotUI : MonoBehaviour
{
    private Image image;
    private TextMeshPro textMeshPro;
    public int intTest = 10;

    void Awake()
    {
        this.image = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        this.textMeshPro = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    public void UpdateUI(ItemDefinition item, int itemQuantity)
    {
        if (itemQuantity == 0)
            textMeshPro.text = "";
        else 
            textMeshPro.text = itemQuantity.ToString();
    }
}
