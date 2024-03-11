using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnnimationMoveScript : MonoBehaviour
{
    private Movement movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        bool isMoving = movement.GetIsMove;
        animator.SetBool("IsMoving", isMoving);
        int dir = movement.GetMyDirToInt();
        if (isMoving)
        {
            animator.SetInteger("Dir", dir);
        }
        else
        {
            spriteRenderer.sprite = sprites[dir];
        }

    }
}
