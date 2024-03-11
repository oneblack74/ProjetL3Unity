using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PortailTP : MonoBehaviour
{
    private GameManager manager;
    public enum State { Activer, Couldown, Desactiver, Start };
    private State state = State.Activer;
    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    [SerializeField] private float maxTimer = 3;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        manager = GameManager.GetInstance();
    }

    void Update()
    {
        if (state == State.Couldown)
        {
            timer += Time.deltaTime;
            if (timer >= maxTimer)
            {
                state = State.Activer;
                spriteRenderer.color = Color.white;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.isTrigger)
            {
                return;
            }
            if (state == State.Activer)
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
            else if (manager.GetSceneIndex == 2 && state == State.Couldown)
            {
                state = State.Desactiver;
                spriteRenderer.color = Color.red;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.isTrigger)
            {
                return;
            }
            if (state == State.Start)
            {
                state = State.Couldown;
                spriteRenderer.color = Color.yellow;
            }
        }
    }

    public State GetState
    {
        get { return state; }
        set { state = value; }
    }
}
