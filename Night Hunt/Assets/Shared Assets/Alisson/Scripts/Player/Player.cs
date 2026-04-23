using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public int playerHealth;
    public int currentHealth;
    public float fatigue = 40f;
    public float currentFatigue;
    public float fatigueLayer = 0f;

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
        currentFatigue = fatigue;

    }

    void Update()
    {

        Sanity.color = new Color(1, 1, 1, fatigueLayer);

        //FATIGUE
        if (!GameItems.Instance.lightToggle)
        {
            ////monster spawn more
            //DecreasingFatigue();

            if (currentFatigue <= 0)
            {
                currentFatigue = 0;
            }
            else if (fatigueLayer >= 1)
            {
                fatigueLayer = 1;
            }
        }
        else
        {
            if (currentFatigue >= 40)
            {
                currentFatigue = 40;
            }
            else if (fatigueLayer <= 0)
            {
                fatigueLayer = 0;
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

    public void IncreasingFatigueBar()
    {
        currentFatigue += 1 * Time.deltaTime;
        fatigueLayer -= 0.2f * Time.deltaTime; //decreasing sanity alpha
    }

    public void DecreasingFatigueBar()
    {
        currentFatigue -= 1 * Time.deltaTime; //decreasing sanity
        fatigueLayer += 0.025f * Time.deltaTime; //increasing sanity alpha
    }
}