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
    private bool gameOverHandled = false;


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
        isPlaying = false;
        ShiftEvents();
    }

    private void Update()
    {
        if (Player.Instance != null && Player.Instance.playerIsDead)
        {
            isPlaying = false;

            if (!gameOverHandled)
            {
                GameOver();
                gameOverHandled = true;
            }

            return;
        }

        gameOverHandled = false;

        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            if (UIElements.Instance != null)
            {
                isPlaying = !UIElements.Instance.StartingTxtPanel.activeSelf && !UIElements.Instance.StartPanel.activeSelf && !UIElements.Instance.EndPanel.activeSelf;
            }
            if (UIElements.Instance.StartPanel.activeSelf == true)
            {
                isPlaying = false;             //idk what is this doing  
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
        GameItems.Instance.currentCoffeeCups = GameItems.Instance.startingCoffeeCups;
        
        GameItems.Instance.coffeeIsFull = true;
        GameItems.Instance.NVBatteryIsFull = true;
        Gun.Instance.isGunEmpty = false;

        Player.Instance.fatigueLayer = 0f;
        DayCounterManager.Instance.DisplayingDay();

        //DayCounterManager.Instance.currentDayDisplay;
    }

    public void PrepareFreshDay()
    {
        isPlaying = false;
            if (MonsterSpawner.Instance != null)
            {
                MonsterSpawner.Instance.ClearAllMonsters();
            }

            if (AnimalSpawner.Instance != null)
            {
                AnimalSpawner.Instance.ClearAllAnimals();
            }

            ShiftEvents();
            //Debug.Log("Fresh day prepared. All monsters and animals cleared.");
    }

    public void GameOver()
    {
        isPlaying = false;

        if (MonsterSpawner.Instance != null)
        {
            MonsterSpawner.Instance.ClearAllMonsters();
        }
        if (AnimalSpawner.Instance != null)
        {
            AnimalSpawner.Instance.ClearAllAnimals();
        }

        UIElements.Instance.EndPanel.SetActive(true);

        UIElements.Instance.MonsterKilledCountDispay();
        UIElements.Instance.AnimalKilledCountDispay();
        UIElements.Instance.DaysSurvivedDisplay();
    }
}
