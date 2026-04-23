using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class GameItems : MonoBehaviour
{
    public bool lightToggle;
    public float startingLightBattery = 100f;
    public float currentLightBattery;


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
        lightToggle = false;
        currentLightBattery = startingLightBattery;
        LightBatteryTxt.SetActive(false);
    }

    void Update()
    {
        //LIGHT BATTERY
        if (lightToggle)
        {
        Player.Instance.GetComponent<Player>().IncreasingSanity();
            

            DecreasingLightBattery();
            if (currentLightBattery <= 0)
            {
                currentLightBattery = 0;
                LightBatteryEmpty();
                Player.Instance.GetComponent<Player>().DecreasingSanity();
            }
            else if (Player.Instance.currentSanity >= 10)
                {
                Player.Instance.currentSanity = 10;
                }
            else if (Player.Instance.sanityLayer <= 0)
                {
                Player.Instance.sanityLayer = 0;
                }
        }
    }

    void LightBatteryEmpty()
    {
        Dark.SetActive(true);
        Player.Instance.GetComponent<Player>().DecreasingSanity();
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

    }
}
