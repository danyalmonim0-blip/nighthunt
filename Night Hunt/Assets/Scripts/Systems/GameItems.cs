using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;


public class GameItems : MonoBehaviour
{
    public bool lightToggle;
    public float startingLightBattery = 100f;
    public float currentLightBattery;
    public bool isLightON;

    public bool nightVision;
    public float startingNVBattery = 20f;
    public float currentNVBattery;
    public bool NVBatteryIsEmpty;
    public bool NVBatteryIsFull;

    public int startingCoffeeCups = 4;
    public int currentCoffeeCups;
    public bool coffeeIsEmpty;
    public bool coffeeIsFull;

    public GameObject Dark;
    public GameObject LightBatteryTxt;
    public GameObject LightSwitchOff;

    public GameObject NightVisionVignette;
    public GameObject NVBattery1;
    public GameObject NVBattery2;
    public GameObject NVBattery3;
    public GameObject NVBattery4;

    public GameObject Coffee;
    public GameObject CoffeeCup1;
    public GameObject CoffeeCup2;
    public GameObject CoffeeCup3;
    public GameObject CoffeeCup4;


    public AudioSource LightSwitchS;

    public static GameItems Instance;
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
        lightToggle = false;
        currentLightBattery = startingLightBattery;
        LightBatteryTxt.SetActive(false);

        currentNVBattery = startingNVBattery;
        currentCoffeeCups = startingCoffeeCups;

        coffeeIsEmpty = false;
        coffeeIsFull = true;

        NVBatteryIsFull = false;
        NVBatteryIsFull = true;
    }

    void Update()
    {
        //When time of the night is reaching 0 all the item values are set to their starting value (default value)
        if (NightTime.Instance.timeOfNight <= 0)
        {
            currentLightBattery = startingLightBattery;
            currentNVBattery = startingNVBattery;
            currentCoffeeCups = startingCoffeeCups;
            LightOffBtn();
        }
        else { }

        //Night Vision button
        if (NVBtn.Instance.isButtonPressed == true)
        {
            NightVisionVignette.SetActive(true);
            DecreaseNVBattery();
        }
        else
        {
            NightVisionVignette.SetActive(false);
        }

        //Light button
        if (lightToggle == true)
        {
            isLightON = true;
        }
        else if (lightToggle == false)
        {
            isLightON = false;
        }

        //Light battery
        if (lightToggle)
        {
               DecreaseLightBattery();
            if (currentLightBattery <= 0)
            {
               currentLightBattery = 0;
               LightBatteryEmpty();
            }
        }
            else { }

        //Checks Night Vision battery
        if (currentNVBattery <= 0)
        {
            currentNVBattery = 0;
            NightVisionVignette.SetActive(false);
            NVBatteryIsEmpty = true;
            NVBatteryIsFull = false;
        }
        else if (currentNVBattery >= 19)
        {
            NVBatteryIsEmpty = false;
            NVBatteryIsFull = true;
            if (NVBatteryIsFull == true)
            {
                //Sets back the battery UI when battery is full
                NVBattery4.SetActive(true);
                NVBattery3.SetActive(true);
                NVBattery2.SetActive(true);
                NVBattery1.SetActive(true);
            }
            else { }
        }
        else
        {
            NVBatteryIsEmpty = false;
            NVBatteryIsFull = false;
            //Debug.Log("NV is being used"); 
        }

        //Hides UI for visible battery state
        if (currentNVBattery <= 15)
        {
            NVBattery4.SetActive(false);
        }
        else { }

        if (currentNVBattery <= 10)
        {
            NVBattery3.SetActive(false);
        }
        else { }

        if (currentNVBattery <= 5)
        {
            NVBattery2.SetActive(false);
        }
        else { }

        if (currentNVBattery <= 0)
        {
            NVBattery1.SetActive(false);
        }
        else { }

        //Checks the state of coffee
        if (currentCoffeeCups <= 0)
        {
            coffeeIsEmpty = true;
            coffeeIsFull = false;

            if (coffeeIsEmpty == true)
            {
                Coffee.GetComponent<Button>().enabled = false;
            }
            else { }
        }
        else if (currentCoffeeCups >= 4)
        {
            coffeeIsFull = true;
            coffeeIsEmpty = false;

            if (coffeeIsFull == true)
            {
                Coffee.GetComponent<Button>().enabled = true;

                //Sets back the coffee cups UI when coffee is full
                CoffeeCup4.SetActive(true);
                CoffeeCup3.SetActive(true);
                CoffeeCup2.SetActive(true);
                CoffeeCup1.SetActive(true);
            }
            else { }
        }
        else
        {
            coffeeIsFull = false;
            coffeeIsEmpty = false;
            //Debug.Log("coffee is being drinked"); 
        }

        //Hides UI for visible coffee state
        if (currentCoffeeCups <= 3)
        {
            CoffeeCup4.SetActive(false);
        }
        else { }

        if (currentCoffeeCups <= 2)
        {
            CoffeeCup3.SetActive(false);
        }
        else { }

        if (currentCoffeeCups <= 1)
        {
            CoffeeCup2.SetActive(false);
        }
        else { }

        if (currentCoffeeCups <= 0)
        {
            CoffeeCup1.SetActive(false);
        }
        else { }

        //Prevents fatigue value to go over or under the max or min value
        if (Player.Instance.currentFatigue >= 40)
        {
            Player.Instance.currentFatigue = 40;
        }
        else if (Player.Instance.fatigueLayer <= 0)
        {
            Player.Instance.fatigueLayer = 0;
        }
    }

    void LightBatteryEmpty()
    {
        Dark.SetActive(true);
        LightBatteryTxt.SetActive(true);
    }

    public void LightOnBtn()
    {
        lightToggle = true;
        Dark.SetActive(false);
        LightSwitchOff.SetActive(true);
        LightSwitchS.Play();
        //Debug.Log("light ON");
    }

    public void LightOffBtn()
    {
        lightToggle = false;
        Dark.SetActive(true);
        LightSwitchOff.SetActive(false);
        LightSwitchS.Play();
        //Debug.Log("light OFF");
    }

    void DecreaseLightBattery()
    {
        currentLightBattery -= 1 * Time.deltaTime;
    }

    public void CoffeeBtn()
    {
        currentCoffeeCups--;
        Player.Instance.DrinkCoffee();
        //Debug.Log(Player.Instance.currentFatigue);
        //Debug.Log("Drinking Coffee");
    }

    void DecreaseNVBattery()
    {
        currentNVBattery -= 1 * Time.deltaTime;
    }
}