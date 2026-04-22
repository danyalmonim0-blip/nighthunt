using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHealth;
    public int currentHealth;
    [SerializeField] public bool playerIsDead;

    public static Player Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerIsDead = false;
        playerHealth = 1;
        currentHealth = playerHealth;
    }

    void Update()
    {
        // lets keep it for testing purposes
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        if (playerIsDead)
        {
            return;
        }

        playerHealth = 0;
        currentHealth = 0;
        playerIsDead = true;

        Debug.Log("Player died.");
    }
}