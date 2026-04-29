using UnityEngine;

public class NightTime : MonoBehaviour
{
    [SerializeField, Range(0, 11)] public float timeOfNight; //Range(0, 121)
    private gameState isPlaying;

    public GameObject GameState;

    public static NightTime Instance;
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
        
    }

    void Update()
    {
        if (gameState.Instance.isPlaying == true )
        {
            GameState.GetComponent<DayCounterManager>().DisplayingDay();
            timeOfNight += 1 * Time.deltaTime;
            timeOfNight %= 11;
            
            if (timeOfNight >= 10)
            {
                DayCounterManager.Instance.dayCounterManagerValues.dayCounter++;
                UIElements.Instance.StartPanel.SetActive(true);
                Invoke("SetSPFalse", 2f);
                timeOfNight = 0;
            }
        }

        
    }

    void SetSPFalse()
    {
        UIElements.Instance.StartPanel.SetActive(false);
    }
}
