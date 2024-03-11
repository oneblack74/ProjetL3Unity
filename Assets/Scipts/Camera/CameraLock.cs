using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target)
        {
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
            transform.position = Vector3.SmoothDamp(targetPosition, targetPosition, ref velocity, 0.05f);
        }
    }

}