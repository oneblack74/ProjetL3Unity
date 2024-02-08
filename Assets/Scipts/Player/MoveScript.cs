using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public enum Dir { Up, Down, Left, Right };
    [SerializeField] private Dir myDir = Dir.Down;
    [SerializeField] private bool isMove = false;


    void Update()
    {
        Move();
    }

    private void Move()
    {
        // récupérer l'input et de movement et modifier le player avec transform.translate
        // modifier myDir et isMove ==> permettra au script MoveAnimationScript de jouer la bonne animation du player
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
