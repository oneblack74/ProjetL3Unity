using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotUI : MonoBehaviour
{
    private Slot slot;
    private Image image;
    private TextMeshPro textMeshPro;
    [SerializeField] private GameObject prefabRef;

    void Awake()
    {
        this.image = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        this.textMeshPro = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
