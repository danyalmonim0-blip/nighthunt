using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameItems : MonoBehaviour
{

    public float startingSanity = 20f;
    public float currentSanity;
    bool lightToggle;
    public float startingLightBattery = 60f;
    public float currentLightBattery;
    public float sanityLayer = 0f;

    public GameObject Dark;
    public Image Sanity;



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
        currentSanity = startingSanity;
        currentLightBattery = startingLightBattery;
    }

    void Update()
    {

        Sanity.color = new Color(1, 1, 1, sanityLayer);
        Debug.Log("Sanity layer" + sanityLayer);


        Debug.Log("Sanity " + currentSanity);
        Debug.Log("Light Battery " + currentLightBattery);

        //SANITY
        if (!lightToggle)
        {
            DecreasingSanity();
            Dark.SetActive(true);

            if (currentSanity <= 0)
            {
                currentSanity = 0;
            }
            else if (sanityLayer >= 1)
            {
                sanityLayer = 1;
            }
        }
        else
        {
            if (currentSanity >= 10)
            {
                currentSanity = 10;
            }
            else if (sanityLayer <= 0)
            {
                sanityLayer = 0;
            }
        }

        //LIGHT BATTERY
        if (lightToggle)
        {
            IncreasingSanity();

            DecreasingLightBattery();
            if (currentLightBattery <= 0)
            {
                currentLightBattery = 0;
                LightBatteryEmpty();
                DecreasingSanity();
            }
            else if (currentSanity >= 10)
                {
                    currentSanity = 10;
                }
            else if (sanityLayer <= 0)
                {
                    sanityLayer = 0;
                }
        }
    }

    void LightBatteryEmpty()
    {
        Dark.SetActive(true);
        DecreasingSanity();
    }

    
    void IncreasingSanity()
    {
        currentSanity += 1 * Time.deltaTime; //increasing sanity
        sanityLayer -= 0.1275f * Time.deltaTime; //decreasing sanity alpha
    }

    void DecreasingSanity()
    {
        currentSanity -= 1 * Time.deltaTime; //decreasing sanity
        sanityLayer += 0.1275f * Time.deltaTime; //increasing sanity alpha
    }

    public void LightOnBtn()
    {
        lightToggle = true;
        Dark.SetActive(false);
    }

    public void LightOffBtn()
    {
        lightToggle = false;
        Dark.SetActive(true);
    }

    void DecreasingLightBattery()
    {
        currentLightBattery -= 1 * Time.deltaTime;
    }
}
