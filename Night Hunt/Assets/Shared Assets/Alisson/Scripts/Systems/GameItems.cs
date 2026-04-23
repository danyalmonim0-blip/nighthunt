using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class GameItems : MonoBehaviour
{
    public bool lightToggle;
    public float startingLightBattery = 100f;
    public float currentLightBattery;
    public bool coffeeBtn = false;
    


    public GameObject Dark;
    public GameObject LightBatteryTxt;

    public AudioSource LightSwitchS;

    public static GameItems Instance;
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
        lightToggle = true;
        currentLightBattery = startingLightBattery;
        LightBatteryTxt.SetActive(false);
    }

    void Update()
    {

        if (!coffeeBtn)
        {
            Player.Instance.GetComponent<Player>().DecreasingFatigueBar();
        }
        else
        {
            Invoke("SetCoffeeBtnFalse", 10f);
            CoffeeBtn();
            Player.Instance.GetComponent<Player>().IncreasingFatigueBar();
        }

        //LIGHT BATTERY
        if (lightToggle)
        {
            //Player.Instance.GetComponent<Player>().IncreasingFatigueBar();
            Dark.SetActive(false);

            DecreasingLightBattery();
            if (currentLightBattery <= 0)
            {
                currentLightBattery = 0;
                LightBatteryEmpty();
                //Player.Instance.GetComponent<Player>().DecreasingFatigue();
            }
        }

        //if (Player.Instance.currentFatigue >= 40)
        //{
        //    Player.Instance.currentFatigue = 40;
        //}
        //else if (Player.Instance.fatigueLayer <= 0)
        //{
        //    Player.Instance.fatigueLayer = 0;
        //}
    }

    void LightBatteryEmpty()
    {
        Dark.SetActive(true);
        //Player.Instance.GetComponent<Player>().DecreasingFatigue();
        LightBatteryTxt.SetActive(true);
    }

    public void LightOnBtn()
    {
        lightToggle = true;
        Dark.SetActive(false);
        LightSwitchS.Play();
    }

    public void LightOffBtn()
    {
        lightToggle = false;
        Dark.SetActive(true);
        LightSwitchS.Play();
    }

    void DecreasingLightBattery()
    {
        currentLightBattery -= 1 * Time.deltaTime;
    }

    public void CoffeeBtn()
    {
        coffeeBtn = true;
    }

    public void SetCoffeeBtnFalse()
    {
        coffeeBtn = false;
    }
}
