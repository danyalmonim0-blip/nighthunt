using UnityEngine;
using UnityEngine.UI;

public class GameItems : MonoBehaviour
{

    //public GameObject Light;
    public float startingSanity = 20f;
    public float currentSanity;
    bool lightToggle;
    public float startingLightBattery = 10f;
    public float currentLightBattery;

    public GameObject Dark;

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
        Debug.Log("Sanity " + currentSanity);
        Debug.Log("Light Battery " + currentLightBattery);

        //SANITY
        if (!lightToggle)
        {
            DecreasingSanity();
            if (currentSanity <= 0)
            {
                currentSanity = 0;
            }
        }
        else
        {
            IncreasingSanity();
            if (currentSanity >= 10)
            {
                currentSanity = 10;
            }
        }

        //LIGHT BATTERY
        if (lightToggle)
        {
            DecreasingLightBattery();
            if (currentLightBattery <= 0)
            {
                currentLightBattery = 0;
                Dark.SetActive(true);
            }
        }
    }

    void IncreasingSanity()
    {
        currentSanity += 1 * Time.deltaTime;
        Dark.SetActive(false);
    }

    void DecreasingSanity()
    {
        currentSanity -= 1 * Time.deltaTime;
        Dark.SetActive(true);
    }

    public void LightOnBtn()
    {
        lightToggle = true;
    }

    public void LightOffBtn()
    {
        lightToggle = false;
    }

    void DecreasingLightBattery()
    {
        currentLightBattery -= 1 * Time.deltaTime;
    }
}
