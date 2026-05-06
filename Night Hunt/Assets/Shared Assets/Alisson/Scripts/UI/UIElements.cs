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
    public GameObject Coffee;
    //public GameObject Light;

    public static UIElements Instance;
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
        //Light.SetActive(true);
        InPanel.SetActive(true);
        EndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (NightTime.Instance.timeOfNight == 15)
        //{
        //    TimeDisplay.text = NightTime.Instance.timeOfNight;
        //}

        //if (StartingTxtPanel.activeSelf == true)
        //{
        //    Time.timeScale = 0f;
        //}
        //else
        //{
            
        //}
    }

    public void MonsterKilledCountDispay()
    {
        MonsterKilledDisplay.text = "Monsters killed: " + MonsterSpawner.Instance.totalKills;
    }

    public void AnimalKilledCountDispay()
    {
        AnimalKilledDisplay.text = "Animals killed: " + AnimalSpawner.Instance.totalAnimalKills;
    }

    public void DaysSurvivedDisplay()
    {
        TimeSurvivedDisplay.text = "Days survived: " + DayCounterManager.Instance.dayCounter;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("btn clicked");
    }

    public void BtnStartingText()
    {
        //StartingTxtPanel.SetActive(false);
        StartPanel.SetActive(true);
        NightTime.Instance.Invoke("SetSPFalse", 2f);
        Time.timeScale = 1f;
        Time.timeScale = 1f;

        //ShiftEvents();
    }

    
}
