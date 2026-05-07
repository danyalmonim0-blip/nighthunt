using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public int playerHealth;
    public int currentHealth;
    public float startingFatigue = 40f;
    public float currentFatigue;
    public float fatigueLayer = 0f;

    [SerializeField] public bool isPlayerDead;

    public Image Fatigue;

    public static Player Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isPlayerDead = false;
        playerHealth = 1;
        currentHealth = playerHealth;
        currentFatigue = startingFatigue;
    }

    void Update()
    {
        //Defines the boolean isPlaying depending on isPlayerDead
        if (!isPlayerDead)
        {
            gameState.Instance.isPlaying = true;
        }
        else
        {
            gameState.Instance.isPlaying = false;
        }

        //Decreases fatigue bar while player is alive
        DecreaseFatigue();

        //Sets back fatigue to default value when shift is ending & fatigue UI overlay to 0
        if (NightTime.Instance.timeOfNight <= 0)
        {
            currentFatigue = startingFatigue;
            fatigueLayer = 0f;
        }
        else
        {
            Fatigue.color = new Color(1, 1, 1, fatigueLayer);
        }

        //Prevents fatigue from going over or under the max(40) or min(0) value
        if (currentFatigue <= 0)
        {
            currentFatigue = 0;
        }
        else if (currentFatigue >= 40)
        {
            currentFatigue = 40;
        }
        else { }

        //Prevents fatigue overlay from going over or under the max(1) or min(0) value
        if (fatigueLayer >= 1)
        {
            fatigueLayer = 1;
        }
        else if (fatigueLayer <= 0)
        {
            fatigueLayer = 0;
        }
        else { }
    }

    //When player dies, set health to 0
    public void Die()
    {
        if (isPlayerDead)
        {
            return;
        }
        else { }

        playerHealth = 0;
        currentHealth = 0;
        isPlayerDead = true;

        Debug.Log("Player died.");
    }

    //Fatigue increases value starts with 40f (when fatigue decreases it decreases the value until 0)
    //Fatigue value increases (player has drank coffee)
    public void IncreaseFatigue()
    {
        currentFatigue += 1 * Time.deltaTime;
        //decreases fatigue opacity
        fatigueLayer -= 0.2f * Time.deltaTime; 
    }

    //Fatigue value decreases (player is getting tired)
    public void DecreaseFatigue()
    {
        currentFatigue -= 1 * Time.deltaTime;
        //increases fatigue opacity
        fatigueLayer += 0.025f * Time.deltaTime; 
    }

    //Drinking coffee increases fatigue value and decreases fatigue overlay
    public void DrinkCoffee()
    {
        currentFatigue = currentFatigue + 20f;
        fatigueLayer = fatigueLayer - 0.5f;
    }
}