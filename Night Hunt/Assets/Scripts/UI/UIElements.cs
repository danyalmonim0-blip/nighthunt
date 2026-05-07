using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElements : MonoBehaviour
{
    public TextMeshProUGUI TimeDisplay;
    public TextMeshProUGUI MonsterKilledDisplay;
    public TextMeshProUGUI AnimalKilledDisplay;
    public TextMeshProUGUI TimeSurvivedDisplay;

    public GameObject StartPanel;
    public GameObject EndPanel;
    public GameObject InPanel;
    public GameObject StartingTxtPanel;
    //public GameObject Coffee;

    public static UIElements Instance;
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

    //Managing the UI panels to display or not
    void Start()
    {
        StartPanel.SetActive(false);
        StartingTxtPanel.SetActive(true);
        InPanel.SetActive(true);
        EndPanel.SetActive(false);
    }

    public void DisplayMonsterKilledCount()
    {
        MonsterKilledDisplay.text = "Monsters killed: " + MonsterSpawner.Instance.totalKills;
    }

    public void DisplayAnimalKilledCount()
    {
        AnimalKilledDisplay.text = "Animals killed: " + AnimalSpawner.Instance.totalAnimalKills;
    }

    public void DisplayDaysSurvived()
    {
        TimeSurvivedDisplay.text = "Days survived: " + DayCounterManager.Instance.dayCounter;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("btn clicked");
    }

    //Hides panel and starts shift after clicking
    public void BtnStartingText()
    {
        gameState.Instance.RunShiftEvents();
        StartPanel.SetActive(true);
        NightTime.Instance.Invoke("SetSPFalse", 2f);
        Time.timeScale = 1f;
        StartingTxtPanel.SetActive(false);
    }
}
