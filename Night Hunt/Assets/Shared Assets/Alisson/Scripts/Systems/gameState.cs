using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class gameState : MonoBehaviour
{
    bool night = true;
    string startingText;
    int dayCounter = 1;
    float currentTime;
    public float startingTime = 5f; //180f for 3min
    public float endPanelLength = 5f;
    public float currentEndPanelLength;
    public float currentStartPanelLength;
    public float startPanelLength = 2f;
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
        Debug.Log("Shift starts");
        
        InPanel.SetActive(false);
        EndPanel.SetActive(false);

        currentTime = startingTime;
        currentStartPanelLength = startPanelLength;

        if (currentStartPanelLength <= 0)
        {
            StartPanel.SetActive(true);
        }
    }

    private void Update()
    {
        startPanelLength -= 1 * Time.deltaTime;
        if (startPanelLength <= 0)
        {
            StartPanel.SetActive(false);
        }

        RealTimeDisplay();
        currentTime -= 1 * Time.deltaTime; //shift timer : shift ends when 0
        if (currentTime <= 0)
        {
            currentTime = startingTime;
            //currentTime = timeSurvived;
            night = false;

            Debug.Log("Shift ends");
            

            DayCounter();

            currentStartPanelLength = startPanelLength;

            startPanelLength -= 1 * Time.deltaTime;
            if (startPanelLength <= 0)
            {
                StartPanel.SetActive(false);
            }
        }
        else if (Player.Instance.playerIsDead == true)
        {
            Debug.Log("Player is dead");
            night = false;

            SetEndPanelStats(); 

            endPanelLength -= 1 * Time.deltaTime;
            if (endPanelLength <= 0)
            {
                EndPanel.SetActive(false);
                SceneManager.LoadScene("MainMenu");
            }

            
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

    void SetEndPanelStats()
    {
        EndPanel.SetActive(true);

        MonsterKilledDisplay.text = "Monster killed:        " + monsterKilled;
        AnimalKilledDisplay.text = "Animal killed:        " + animalKilled;
        TimeSurvivedDisplay.text = "Time survived:        " + timeSurvived;

    }



    //public void Continue()
    //{
    //    SceneManager.LoadScene("Game1");
    //}

    //public void BackMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
}
