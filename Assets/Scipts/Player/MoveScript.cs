using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public enum Dir { Up, Down, Left, Right };
    [SerializeField] private Dir myDir = Dir.Down;
    [SerializeField] private bool isMove = false;
    private GameManager manager;
    private PlayerInput inputs;
    private InputAction moveAction;

    void Start()
    {
        manager = GameManager.GetInstance();
        inputs = manager.GetInputs;

        moveAction = inputs.actions["Move"];
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // récupérer l'input avec inputmanager et de movement et modifier le player avec transform.translate
        // modifier myDir et isMove ==> permettra au script MoveAnimationScript de jouer la bonne animation du player
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        transform.Translate(moveValue * moveSpeed * Time.deltaTime);
        if (moveValue.x > 0)
        {
            myDir = Dir.Right;
            isMove = true;
        }
        else if (moveValue.x < 0)
        {
            myDir = Dir.Left;
            isMove = true;
        }
        else if (moveValue.y > 0)
        {
            myDir = Dir.Up;
            isMove = true;
        }
        else if (moveValue.y < 0)
        {
            myDir = Dir.Down;
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }

    public float GetMoveSpeed
    {
        get { return moveSpeed; }
    }

    public Dir GetMyDir
    {
        get { return myDir; }
    }

    public bool GetIsMove
    {
        get { return isMove; }
    }
}
