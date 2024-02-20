using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    SlotUI slotUI;

    void Start()
    {
        slotUI = GetComponent<SlotUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            slotUI.LeftClick();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            slotUI.MiddleClick();
        else if (eventData.button == PointerEventData.InputButton.Right)
            slotUI.RightCLick();
    }
}