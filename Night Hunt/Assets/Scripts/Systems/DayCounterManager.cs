using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayCounterManager : MonoBehaviour
{
    public TextMeshProUGUI dayDisplay;

    public int dayCounter;
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
        else
        {
            Destroy(gameObject);
        }
    }

    //Sets first day to 1 & shows day display panel and invokes SetSPFalse() to disable panel after 2 secs
    private void Start()
    {
        dayCounter = 1;
        UIElements.Instance.StartPanel.SetActive(true);
        NightTime.Instance.Invoke("SetSPFalse", 2f);
        currentDayDisplayTimer = dayDisplayTimer;
    }

    //Displays text of day
    public void DisplayDay()
    {
        dayDisplay.text = "Day " + dayCounter.ToString();
    }
}
