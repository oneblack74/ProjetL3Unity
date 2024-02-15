using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(MoveScript))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnnimationMoveScript : MonoBehaviour
{
    private MoveScript moveScript;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    void Awake()
    {
        moveScript = GetComponent<MoveScript>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        bool isMoving = moveScript.GetIsMove;
        animator.SetBool("IsMoving", isMoving);
        int dir = moveScript.GetMyDirToInt();
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
