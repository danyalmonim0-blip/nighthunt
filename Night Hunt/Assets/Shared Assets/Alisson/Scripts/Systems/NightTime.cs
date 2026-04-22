using UnityEngine;

public class NightTime : MonoBehaviour
{
    [SerializeField, Range(0, 9)] public float timeOfNight;
    private gameState isPlaying;

    //public DayCounterManager dayCounting;
   

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
        gameState.Instance.StartPanel.SetActive(true);
        Invoke("SetSPFalse", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.Instance.isPlaying == true )
        {
            GameState.GetComponent<DayCounterManager>().DisplayingDay();
            timeOfNight += 1 * Time.deltaTime;
            timeOfNight %= 9;
            Debug.Log("day Display");
            
            if (timeOfNight >= 8)
            {
                DayCounterManager.Instance.dayCounterManagerValues.dayCounter++;
                Debug.Log("day " + DayCounterManager.Instance.dayCounterManagerValues.dayCounter);
                gameState.Instance.StartPanel.SetActive(true);
                Invoke("SetSPFalse", 2f);
                timeOfNight = 0;
            }
        }
    }

    void SetSPFalse()
    {
        gameState.Instance.StartPanel.SetActive(false);
    }
}
