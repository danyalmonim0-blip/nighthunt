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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHealth = 0;
        }
        else if (playerHealth == 0)
        {
            playerIsDead = true;
        }
    }

        

        //add damage mechanic


    }
