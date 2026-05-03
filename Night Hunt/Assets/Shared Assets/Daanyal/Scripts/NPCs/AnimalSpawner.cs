using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform hutAnimalSpawn;

    [SerializeField] private int activeAnimals = 0;
    [SerializeField] public int totalAnimalKills = 0;
    //[SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;



    public Transform[] stage1SpotsAn;
    public Transform[] stage2SpotsAn;
    public Transform[] stage3SpotsAn;

    public float randomNum;
    //private float timer = 0f;

    public static AnimalSpawner Instance;

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
        currentTimerRand = GetCurrentAnimalSpawnInterval();
    }

    private void Update()
    {
        if (gameState.Instance == null || gameState.Instance.isPlaying == false)
        {
            return;
        }

        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            currentTimerRand -= 1 * Time.deltaTime;
            if (currentTimerRand <= 0)
            {
                RandomNum();
                currentTimerRand = GetCurrentAnimalSpawnInterval();
            }
        }
    }

    public float GetCurrentAnimalSpawnInterval()
    {
        float time = NightTime.Instance.timeOfNight;

        // Later in the night = faster spawn checks
        if (time >= 90f)
        {
            return 1.5f;
        }
        else if (time >= 60f)
        {
            return 2f;
        }
        else if (time >= 30f)
        {
            return 2.5f;
        }
        else
        {
            return 3f;
        }
    }

    public void RandomNum()
    {
        randomNum = Random.Range(1, 13);
        //Debug.Log(randomNum);

        //int day = DayCounterManager.Instance.dayCounter;

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            if (randomNum >= 1 && randomNum <= 2)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            if (randomNum >= 1 && randomNum <= 3)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
        {
            if (randomNum >= 1 && randomNum <= 4)
            {
                SpawnAnimal();
            }
        }

        //Debug.Log("Night time: " + NightTime.Instance.timeOfNight + " | Spawn interval: " + GetCurrentAnimalSpawnInterval() + " | Roll: " + randomNum);
    }

    public void SpawnAnimal()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        GameObject newAnimal = Instantiate(animalPrefab, chosenSpawnPoint.position, Quaternion.identity, hutAnimalSpawn);

        activeAnimals++;
        Debug.Log("Animal spawned. Active Animals: " + activeAnimals);

        ClickableTarget clickableTarget = newAnimal.GetComponent<ClickableTarget>();
        if (clickableTarget != null)
        {
            clickableTarget.SetAnimalSpawner(this);
        }
        AnimalBehaviour animalBehaviour = newAnimal.GetComponent<AnimalBehaviour>();
        if (animalBehaviour != null)
        {
            animalBehaviour.spawnPoint = chosenSpawnPoint;
            animalBehaviour.stage1SpotsAn = stage1SpotsAn;
            animalBehaviour.stage2SpotsAn = stage2SpotsAn;
            animalBehaviour.stage3SpotsAn = stage3SpotsAn;
            animalBehaviour.animalSpawner = this;
        }
        else
        {
            Debug.LogWarning("Spawned animal does not have AnimalBehaviour");
        }
    }

    /*public void AnimalKilledCnt()
    {
        activeAnimals--;

        if (activeAnimals < 0)
        {
            activeAnimals = 0;
        }

        totalKills++;
        Debug.Log("Enemy killed. Total kills: " + totalKills);
    }*/

    public void RegisterAnimalKilled()
            {
                activeAnimals--;

                if (activeAnimals < 0)
                {
                    activeAnimals = 0;
                }

                totalAnimalKills++;
                Debug.Log("Animal killed. Total animal kills: " + totalAnimalKills);
            }

    public void RegisterAnimalDespawn()
    {
        activeAnimals--;

        if (activeAnimals < 0)
        {
            activeAnimals = 0;
        }
        Debug.LogAssertion("Animal disappeard after 30 sec");
    }

    public void ClearAllAnimals()
    {
        if (hutAnimalSpawn == null)
        {
            return;
        }
        for (int i = hutAnimalSpawn.childCount - 1; i >= 0; i--)
        {
            Destroy(hutAnimalSpawn.GetChild(i).gameObject);
        }
        activeAnimals = 0;
        currentTimerRand = GetCurrentAnimalSpawnInterval();

        Debug.Log("All animals cleared.");
    }

    public void ResetAnimalTimer()
    {
        currentTimerRand = GetCurrentAnimalSpawnInterval();
    }
}