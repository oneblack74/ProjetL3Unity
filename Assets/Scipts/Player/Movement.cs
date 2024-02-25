using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speedTmp;
    [SerializeField] private float speed;

    public enum Dir { Up, Down, Left, Right };
    [SerializeField] private Dir myDir = Dir.Down;
    [SerializeField] private bool isMoving = false;

    void Start()
    {
    }


    public void Move(Vector3 moveValue)
    {
        // récupère l'input avec inputmanager et de movement et modifier le player avec transform.translate
        // modifier myDir et isMoving ==> permettra au script MoveAnimationScript de jouer la bonne animation du player

        transform.Translate(speed * Time.deltaTime * moveValue);
        if (moveValue.x > 0)
        {
            myDir = Dir.Right;
            isMoving = true;
        }
        else if (moveValue.x < 0)
        {
            myDir = Dir.Left;
            isMoving = true;
        }
        else if (moveValue.y > 0)
        {
            myDir = Dir.Up;
            isMoving = true;
        }
        else if (moveValue.y < 0)
        {
            myDir = Dir.Down;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public float GetSpeedTmp
    {
        get { return speedTmp; }
    }

    public float GetSpeed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Dir GetMyDir
    {
        get { return myDir; }
    }

    public int GetMyDirToInt()
    {

        if (myDir == Dir.Up) return 0;
        if (myDir == Dir.Right) return 1;
        if (myDir == Dir.Down) return 2;
        return 3;

    }

    public bool GetIsMove
    {
        get { return isMoving; }
    }



}
