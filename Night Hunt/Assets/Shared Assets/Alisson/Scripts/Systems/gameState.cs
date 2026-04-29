using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class gameState : MonoBehaviour
{
    string startingText;
    //public float currentDayDisplayTimer;

    public bool isPlaying;
    
    public Player playerHealth;
    public Player currentHealth;
    public Player playerIsDead;

    //public DayCounterManager dayDisplay;

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
        
    }

    private void Update()
    {

        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            if (UIElements.Instance.StartPanel.activeSelf == true)
            {
                isPlaying = false;
            }
            isPlaying = true;
        }

        if (Player.Instance.playerIsDead == true)
        {
            GameOver();
        }
    }


    public void ShiftEvents()
    {
        GameItems.Instance.LightBatteryTxt.SetActive(false);
        Player.Instance.currentFatigue = Player.Instance.startingFatigue;
        GameItems.Instance.currentLightBattery = GameItems.Instance.startingLightBattery;
        GameItems.Instance.currentNVBattery = GameItems.Instance.startingNVBattery;
        Player.Instance.fatigueLayer = 0f;


        //currentDayDisplayTimer = dayDisplayTimer;
    }
    public void ShiftEnds()
    {

    }

    public void GameOver()
    {
        UIElements.Instance.EndPanel.SetActive(true);

        UIElements.Instance.GetComponent<UIElements>().MonsterKilledCountDispay();
        UIElements.Instance.GetComponent<UIElements>().AnimalKilledCountDispay();
        UIElements.Instance.GetComponent<UIElements>().DaysSurvivedDisplay();
    }
}
