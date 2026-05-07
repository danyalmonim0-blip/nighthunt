using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class gameState : MonoBehaviour
{
    public bool isPlaying;
    
    public Player playerHealth;
    public Player currentHealth;
    public Player isPlayerDead;

    private bool gameOverHandled = false;

    public static gameState Instance;
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

    private void Update()
    {
        //Checking if player is alive
        if (Player.Instance != null && Player.Instance.isPlayerDead)
        {
            if (!gameOverHandled)
            {
                GameOver();
                gameOverHandled = true;
            }
        }
        else
        {
            gameOverHandled = false;
        }


        //Welcoming text displaying before first day
        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            if (UIElements.Instance != null)
            {
                isPlaying = !UIElements.Instance.StartingTxtPanel.activeSelf && !UIElements.Instance.StartPanel.activeSelf && !UIElements.Instance.EndPanel.activeSelf;
            }
            if (UIElements.Instance.StartPanel.activeSelf == true)
            {
                isPlaying = false;             
            }
            else { }

            isPlaying = true;
        }
        else { } //If Text is null = can make shift start normally here
    }

    //Defines what happened at each shifts
    public void RunShiftEvents()
    {
        isPlaying = true;

        GameItems.Instance.LightBatteryTxt.SetActive(false);

        Player.Instance.currentFatigue = Player.Instance.startingFatigue;
        GameItems.Instance.currentLightBattery = GameItems.Instance.startingLightBattery;
        GameItems.Instance.currentNVBattery = GameItems.Instance.startingNVBattery;
        GameItems.Instance.currentCoffeeCups = GameItems.Instance.startingCoffeeCups;
        
        GameItems.Instance.coffeeIsFull = true;
        GameItems.Instance.NVBatteryIsFull = true;
        Gun.Instance.isGunEmpty = false;

        Player.Instance.fatigueLayer = 0f;

        DayCounterManager.Instance.DisplayDay();
    }

    //Clears all the monsters/animals at the end of the shift
    public void PrepareFreshDay()
    {
        isPlaying = false;

            if (MonsterSpawner.Instance != null)
            {
                MonsterSpawner.Instance.ClearAllMonsters();
            }
            else { }

            if (AnimalSpawner.Instance != null)
            {
                AnimalSpawner.Instance.ClearAllAnimals();
            }
            else { }

        RunShiftEvents();
    }

    //Displays GameOver panel and stats
    public void GameOver()
    {
        if (MonsterSpawner.Instance != null)
        {
            MonsterSpawner.Instance.ClearAllMonsters();
        }
        else { }

        if (AnimalSpawner.Instance != null)
        {
            AnimalSpawner.Instance.ClearAllAnimals();
        }
        else { }

        UIElements.Instance.EndPanel.SetActive(true);

        UIElements.Instance.DisplayMonsterKilledCount();
        UIElements.Instance.DisplayAnimalKilledCount();
        UIElements.Instance.DisplayDaysSurvived();
    }
}
