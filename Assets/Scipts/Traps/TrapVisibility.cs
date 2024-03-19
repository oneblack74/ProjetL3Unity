using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TrapVisibility : MonoBehaviour
{
    // SerializeField
    [SerializeField] private float visibilityStart = 0.5f;
    [SerializeField] private float visibilityEnd = 1.0f;

    // Variables
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, visibilityStart);
    }

    public void ChangeVisibility()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, visibilityEnd);
    }
}
