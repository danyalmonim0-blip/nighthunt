using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class GameItems : MonoBehaviour
{
    public bool lightToggle;
    public float startingLightBattery = 100f;
    public float currentLightBattery;
    //public bool coffeeBtn = false;
    public bool nightVision;
    public float startingNVBattery = 20f;
    public float currentNVBattery;
    public int startingCoffeeCups = 4;
    public int currentCoffeeCups;
    public bool coffeeIsEmpty;

    //public bool isNVOn = false;


    public GameObject Dark;
    public GameObject LightBatteryTxt;
    public GameObject NightVisionVignette;
    public GameObject Coffee;
    public GameObject NVBattery1;
    public GameObject NVBattery2;
    public GameObject NVBattery3;
    public GameObject NVBattery4;
    public GameObject CoffeeCup1;
    public GameObject CoffeeCup2;
    public GameObject CoffeeCup3;
    public GameObject CoffeeCup4;
    public GameObject LightSwitchOff;


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
        //lightToggle = true;
        LightOffBtn();
        currentLightBattery = startingLightBattery;
        LightBatteryTxt.SetActive(false);
        currentNVBattery = startingNVBattery;
        currentCoffeeCups = startingCoffeeCups;
        coffeeIsEmpty = false;

    }

    void Update()
    {
        if (NightTime.Instance.timeOfNight <= 0)
        {
            currentLightBattery = startingLightBattery;
            currentNVBattery = startingNVBattery;
            currentCoffeeCups = startingCoffeeCups;
            LightOffBtn();
        }

        if (NVBtn.Instance.buttonPressed == true)
        {
            NightVisionVignette.SetActive(true);
            DecreasingNVBattery();
            MonsterColor.Instance.GetComponent<MonsterColor>().MonsterClr();
        }
        else
        {
            NightVisionVignette.SetActive(false);
        }

        //LIGHT BATTERY
        if (lightToggle)
        {
            DecreasingLightBattery();
            if (currentLightBattery <= 0)
            {
                currentLightBattery = 0;
                LightBatteryEmpty();
            }
        }

        if (currentNVBattery <= 0)
        {
            currentNVBattery = 0;
            NightVisionVignette.SetActive(false);
        }

        if (Player.Instance.currentFatigue >= 40)
        {
            Player.Instance.currentFatigue = 40;
        }
        else if (Player.Instance.fatigueLayer <= 0)
        {
            Player.Instance.fatigueLayer = 0;
        }

        if (currentCoffeeCups <= 0)
        {
            coffeeIsEmpty = true;
        }

        if (coffeeIsEmpty == true)
        {
            Coffee.GetComponent<Button>().enabled = false;
            Debug.Log("Coffee is empty");
        }

        //Night Vision Battery events
        if (currentNVBattery <= 15)
        {
            NVBattery4.SetActive(false);
        }

        if (currentNVBattery <= 10)
        {
            NVBattery3.SetActive(false);
        }

        if (currentNVBattery <= 5)
        {
            NVBattery2.SetActive(false);
        }

        if (currentNVBattery <= 0)
        {
            NVBattery1.SetActive(false);
        }

        //Coffee Cups Quantity events
        if (currentCoffeeCups <= 3)
        {
            CoffeeCup4.SetActive(false);
        }

        if (currentCoffeeCups <= 2)
        {
            CoffeeCup3.SetActive(false);
        }

        if (currentCoffeeCups <= 1)
        {
            CoffeeCup2.SetActive(false);
        }

        if (currentCoffeeCups <= 0)
        {
            CoffeeCup1.SetActive(false);
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

    void DecreasingLightBattery()
    {
        currentLightBattery -= 1 * Time.deltaTime;
    }

    public void CoffeeBtn()
    {
        currentCoffeeCups--;
        Player.Instance.currentFatigue = Player.Instance.currentFatigue + 20f;
        Player.Instance.fatigueLayer = Player.Instance.fatigueLayer - 0.5f;
        Debug.Log(Player.Instance.currentFatigue);
        //Debug.Log("Coffee Drank");
        Crosshair.Instance.swayAmplitude = Crosshair.Instance.swayAmplitude + 0.5f;
        Crosshair.Instance.swayFrequency = Crosshair.Instance.swayFrequency + 0.5f;

    }

    void DecreasingNVBattery()
    {
        currentNVBattery -= 1 * Time.deltaTime;
    }



    //public void NightVisionBtn()
    //{
    //    if (NVBtn.Instance.buttonPressed == false)
    //    {
    //        MonsterSpawner.Instance.ChangeNPCColorBla();
    //        Debug.Log("Color Black");
    //    }
    //    else
    //    {
    //        MonsterSpawner.Instance.ChangeNPCColorGre();
    //        Debug.Log("Color Green");
    //    }
    //}
}
