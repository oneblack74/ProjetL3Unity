using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float staminaConsumption;
    private Gameobject.GetComponent<Stamina>() staminaObject;
    private Gameobject.GetComponent<Move>() moveObject;
    private float timer;

    public Sprint(float sprintMultiplier, float staminaConsumption) {
        this.sprintMultiplier = sprintMultiplier;
        this.staminaConsumption = staminaConsumption;
    }

    public void Sprint(Vector2 vector) {
        staminaObject.TryDrainStamina(staminaConsumption);
        moveObject.move(moveObject.GetMoveSpeed*sprintMultiplier, vector);
        
    }

    void Update() {
        timer += Time.deltaTime;
        
        
    }
}
