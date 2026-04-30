using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayCounterManager : MonoBehaviour
{

    public TextMeshProUGUI dayDisplay;
    //public DayCounter dayCounterManagerValues;
    public int dayCounter = 1;
    public float dayDisplayTimer = 2f;
    public float currentDayDisplayTimer;

    public GameObject StartPanel;


    public static DayCounterManager Instance;
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

    private void Start()
    {
        UIElements.Instance.StartPanel.SetActive(true);
        NightTime.Instance.Invoke("SetSPFalse", 2f);
        currentDayDisplayTimer = dayDisplayTimer;
        dayCounter = 1;
    }
    private void Update()
    {
        
    }

    public void DisplayingDay()
    {
        //currentDayDisplayTimer = dayDisplayTimer;

        dayDisplay.text = "Day " + dayCounter.ToString();
    }
}
