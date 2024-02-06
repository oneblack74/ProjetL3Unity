using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedRate;
    private Gameobject.GetComponent<Character>() characterObject;

    private float timer;

    public float GetMoveSpeed {
        get {return moveSpeed;};
    }

    public void Move(float speed, Vector2 vector) {
        timer = 0f;
        if (timer > moveSpeedRate){
            timer = 0f;
            characterObject.transform.position.x += vector.x*speed;
            characterObject.transform.position.y += vector.y*speed;
        }
    }

    void Update() {
        timer+=Time.deltaTime;
    }
}
