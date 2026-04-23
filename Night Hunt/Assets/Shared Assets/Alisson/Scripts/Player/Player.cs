using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public int playerHealth;
    public int currentHealth;
    public float startingSanity = 20f;
    public float currentSanity;
    public float sanityLayer = 0f;

    [SerializeField] public bool playerIsDead;

    public Image Sanity;


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
        currentSanity = startingSanity;
    }

    void Update()
    {
        Sanity.color = new Color(1, 1, 1, sanityLayer);

        //SANITY
        if (!GameItems.Instance.lightToggle)
        {
            DecreasingSanity();
            GameItems.Instance.Dark.SetActive(true);

            if (currentSanity <= 0)
            {
                currentSanity = 0;
            }
            else if (sanityLayer >= 1)
            {
                sanityLayer = 1;
            }
        }
        else
        {
            if (currentSanity >= 10)
            {
                currentSanity = 10;
            }
            else if (sanityLayer <= 0)
            {
                sanityLayer = 0;
            }
        }



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

    public void IncreasingSanity()
    {
        currentSanity += 1 * Time.deltaTime; //increasing sanity
        sanityLayer -= 0.1275f * Time.deltaTime; //decreasing sanity alpha
    }

    public void DecreasingSanity()
    {
        currentSanity -= 1 * Time.deltaTime; //decreasing sanity
        sanityLayer += 0.1275f * Time.deltaTime; //increasing sanity alpha
    }
}