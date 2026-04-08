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
    int dayCounter = 0;
    public float currentTime;
    public float startingTime = 7f; //180f for 3min
    public float gameOverScrnLength = 2f;
    public float currentGameOverScrnLength;
    public float currentStartScrnLength;
    public float startScrnLength = 2f;
    int monsterKilled;
    int animalKilled;
    float timeSurvived;

    public GameObject StartPanel;
    public GameObject EndPanel;
    public GameObject InPanel;


    public TextMeshProUGUI DayDisplay;
    public TextMeshProUGUI TimeDisplay;
    public TextMeshProUGUI MonsterKilledDisplay;
    public TextMeshProUGUI AnimalKilledDisplay;
    public TextMeshProUGUI TimeSurvivedDisplay;

    public Player playerHealth;
    public Player currentHealth;
    public Player playerIsDead;

    void Start()
    {
        InPanel.SetActive(true);
        EndPanel.SetActive(false);

        ShiftEvents();
    }

    private void Update()
    {
        RealTimeDisplay();

        currentStartScrnLength -= 1 * Time.deltaTime;
        if (currentStartScrnLength <= 0)
        {
            StartPanel.SetActive(false);
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
        currentGameOverScrnLength = gameOverScrnLength;
        currentGameOverScrnLength -= 1 * Time.deltaTime;

        if (gameOverScrnLength <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
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

    
}
