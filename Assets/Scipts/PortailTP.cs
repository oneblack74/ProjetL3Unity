using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortailTP : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (manager.GetSceneIndex == 1)
            {
                manager.ChangeScene(2);
            }
            else
            {
                manager.ChangeScene(1);
            }
        }
    }
}
