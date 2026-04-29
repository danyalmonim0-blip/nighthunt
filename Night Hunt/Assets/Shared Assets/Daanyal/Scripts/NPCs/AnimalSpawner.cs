using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform hutAnimalSpawn;

    [SerializeField] private int activeAnimals = 0;
    [SerializeField] public int totalAnimalKills = 0;
    [SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;



    public Transform[] stage1SpotsAn;
    public Transform[] stage2SpotsAn;
    public Transform[] stage3SpotsAn;

    public float randomNum;
    private float timer = 0f;

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
        currentTimerRand = timerRand;
    }

    public void RandomNum()
    {
        randomNum = Random.Range(1, 13);
        //Debug.Log(randomNum);

        currentTimerRand = timerRand;

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            if (randomNum == 1 || randomNum == 2)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3 || randomNum == 4)
            {
                SpawnAnimal();
            }
        }
    }

    private void Update()
    {
        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            currentTimerRand -= 1 * Time.deltaTime;
            if (currentTimerRand <= 0)
            {
                RandomNum();
            }
        }
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
        Debug.LogAssertion("animal disappeard after 30 sec");
    }
}