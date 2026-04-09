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
    public float startScrnLength = 2f;
    public float currentStartScrnLength;


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
        Time.timeScale = 0f;

        InPanel.SetActive(true);
        EndPanel.SetActive(false);

    }

    private void Update()
    {
        RealTimeDisplay();

        currentStartScrnLength -= 1 * Time.deltaTime;
        if (currentStartScrnLength <= 0)
        {
            StartPanel.SetActive(false);
        }

        // GAME LOOP
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


    public void ShiftEvents()
    {
        DayCounter();

        StartPanel.SetActive(true);
        currentStartScrnLength = startScrnLength;

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
    {
        TimeDisplay.text = "Time left " + currentTime;
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

        Time.timeScale = 1f;
    }
}
