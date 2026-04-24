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
    public float startingNVBattery = 50f;
    public float currentNVBattery;

    public GameObject Dark;
    public GameObject LightBatteryTxt;
    public GameObject NightVisionVignette;
   

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
        currentNVBattery = startingNVBattery;

    }

    void Update()
    {

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

        if (currentNVBattery <= 0)
        {
            currentNVBattery = 0;
            //LightBatteryEmpty();
            //Player.Instance.GetComponent<Player>().DecreasingFatigue();
        }

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
        Player.Instance.currentFatigue = Player.Instance.currentFatigue + 20f;
        Player.Instance.fatigueLayer = Player.Instance.fatigueLayer - 0.5f;
        Debug.Log(Player.Instance.currentFatigue);
        Debug.Log("Button Pressed");
    }

    void DecreasingNVBattery()
    {
        currentNVBattery -= 1 * Time.deltaTime;
    }
}
