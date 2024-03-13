using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private IInteractable interactableComponent;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite nearSprite;
    private bool isNear = false;

    public void Interact()
    {
        interactableComponent.Interact();
    }

    public void ChangeNear()
    {
        isNear = !isNear;
        if (isNear)
        {
            GetComponent<SpriteRenderer>().sprite = nearSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }
    }

    public Sprite GetDefaultSprite
    {
        get { return defaultSprite; }
        set { defaultSprite = value; }
    }

    public Sprite GetNearSprite
    {
        get { return nearSprite; }
        set { nearSprite = value; }
    }
}
