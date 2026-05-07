using UnityEngine;

public class NightTime : MonoBehaviour
{
    [SerializeField, Range(0, 121)] public float timeOfNight; //Range(0, 121)

    public GameObject GameState;

    public static NightTime Instance;
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

    void Update()
    {
        //Starts the timer when player is playing & calls "PrepareFreshDay()" function when night ends
        if (gameState.Instance.isPlaying == true )
        {
            DayCounterManager.Instance.DisplayDay();
            timeOfNight += 1 * Time.deltaTime;
            timeOfNight %= 121;

            if (timeOfNight >= 120)
            {
                gameState.Instance.PrepareFreshDay();

                DayCounterManager.Instance.dayCounter++;
                UIElements.Instance.StartPanel.SetActive(true);
                Invoke("SetSPFalse", 2f);
                timeOfNight = 0;
            }
            else { }
        }
        else { }
    }

    //Prevents monsters/animals from spawning behind panel
    void SetSPFalse()
    {
        UIElements.Instance.StartPanel.SetActive(false);

        if (MonsterSpawner.Instance != null)
        {
            MonsterSpawner.Instance.ResetMonsterTimer();
        }
        else { }

        if (AnimalSpawner.Instance != null)
        {
            AnimalSpawner.Instance.ResetAnimalTimer();
        }
        else { }
    }
}
