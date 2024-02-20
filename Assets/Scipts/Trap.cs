using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private enum trapType { Damage, Slow };
    [SerializeField] private trapType type;
    private HealthControl healthObject;
    private MoveScript moveObject;
    private float trapDamage = 10.0f;

    void Awake() {
        moveObject   = GameObject.Find("Player").   GetComponent<MoveScript>();
        healthObject = GameObject.Find("Player").GetComponent<HealthControl>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            switch (type) {
                case trapType.Damage:
                    procDamageTrap(trapDamage);
                    break;
                case trapType.Slow:
                    procSlowTrap(0.7f, 3f);
                    break;
                default:
                    Debug.LogWarning("Invalid trap type !");
                    break;
            }
        }
    }

    private void procDamageTrap(float damage) {
        healthObject.DrainHealth(damage);
    }

    private void procSlowTrap(float multiplier, float timeInSeconds) {
        moveObject.ApplyModifier(multiplier, timeInSeconds);
    }
}
