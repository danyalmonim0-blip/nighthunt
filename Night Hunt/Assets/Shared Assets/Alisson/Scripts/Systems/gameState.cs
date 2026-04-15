using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class gameState : MonoBehaviour
{
    //bool isPlayerAlive;
    string startingText;
    public int dayCounter = 0;
    public float currentTime;
    public float startingTime = 120f; //180f for 3min
    public float dayDisplayTimer = 2f;
    public float currentDayDisplayTimer;
    


    public GameObject StartPanel;
    public GameObject EndPanel;
    public GameObject InPanel;
    public GameObject StartingTxtPanel;

    public TextMeshProUGUI DayDisplay;
    public TextMeshProUGUI TimeDisplay;
    public TextMeshProUGUI MonsterKilledDisplay;
    public TextMeshProUGUI AnimalKilledDisplay;
    public TextMeshProUGUI TimeSurvivedDisplay;

    public Player playerHealth;
    public Player currentHealth;
    public Player playerIsDead;

    public static gameState Instance;
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
        InPanel.SetActive(true);
        EndPanel.SetActive(false);
    }

    private void Update()
    {

        RealTimeDisplay();

        // GAME LOOP
        if (StartingTxtPanel.activeSelf == false)
        {
            currentDayDisplayTimer -= 1 * Time.deltaTime;
            if (currentDayDisplayTimer <= 0)
            {
                StartPanel.SetActive(false);
                currentDayDisplayTimer = 0;
            }

            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0)
            {
                ShiftEnds();
                ShiftEvents();
            }
            else if (Player.Instance.playerIsDead == true)
            {
                GameOver();
            }
        }
    }


    public void ShiftEvents()
    {
        DayCounter();
        GameItems.Instance.currentSanity = 10f;
        GameItems.Instance.sanityLayer = 0f;
        GameItems.Instance.currentLightBattery = 10f;

        StartPanel.SetActive(true);
        currentDayDisplayTimer = dayDisplayTimer;

        currentTime = startingTime;
        Debug.Log("Shift starts");
    }
    public void ShiftEnds()
    {
        Debug.Log("Shift ends");

        currentTime = startingTime;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        EndPanel.SetActive(true);

        MonsterKilledCountDispay();
        AnimalKilledCountDispay();
        DaysSurvivedDisplay();
    }

    void DayCounter()
    {
        Debug.Log("Day " + dayCounter);
        dayCounter++;
        DayCountDisplay();
    }

    void DayCountDisplay()
        {
            DayDisplay.text = "Day " + dayCounter;
        }

    void RealTimeDisplay() 
    { //doesnt work
        if (currentTime == 120)
        {
            TimeDisplay.text = "10PM";
        }
        else if (currentTime == 105)
        {
            TimeDisplay.text = "11PM";
        }
        else if (currentTime == 90)
        {
            TimeDisplay.text = "12AM";
        }
        else if (currentTime == 75)
        {
            TimeDisplay.text = "1AM";
        }
        else if (currentTime == 60)
        {
            TimeDisplay.text = "2AM";
        }
        else if (currentTime == 45)
        {
            TimeDisplay.text = "3AM";
        }
        else if (currentTime == 30)
        {
            TimeDisplay.text = "4AM";
        }
        else if (currentTime == 15)
        {
            TimeDisplay.text = "5AM";
        }
    }

    void MonsterKilledCountDispay()
    {
        MonsterKilledDisplay.text = "Monsters killed: " + MonsterSpawner.Instance.totalKills;
    }

    void AnimalKilledCountDispay()
    {
        AnimalKilledDisplay.text = "Animals killed: ";
    }

    void DaysSurvivedDisplay()
    {
        TimeSurvivedDisplay.text = "Days survived: " + dayCounter;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BtnStartingText()
    {
        StartingTxtPanel.SetActive(false);
        ShiftEvents();
    }
}
