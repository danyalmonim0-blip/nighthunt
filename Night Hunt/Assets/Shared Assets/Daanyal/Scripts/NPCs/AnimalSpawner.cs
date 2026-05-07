using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform hutAnimalSpawn;

    [SerializeField] private int activeAnimals = 0;
    [SerializeField] public int totalAnimalKills = 0;
    [SerializeField] private float currentTimerRand;

    public float randomNum;

    // These are the movement spots where Animal prefab can move.
    public Transform[] stage1SpotsAn;
    public Transform[] stage2SpotsAn;
    public Transform[] stage3SpotsAn;

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
        else { }

        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            currentTimerRand -= 1 * Time.deltaTime;
            if (currentTimerRand <= 0)
            {
                ChooseRandomNum();
                currentTimerRand = GetCurrentAnimalSpawnInterval();
            }
        }
        else { }
    }

    public float GetCurrentAnimalSpawnInterval()
    {
        //This function is responsible for increasing the spawn ratio within the day as night time increases.
        float time = NightTime.Instance.timeOfNight;

        if (time >= 90f)
        {
            return 3.5f;
        }
        else if (time >= 60f)
        {
            return 4f;
        }
        else if (time >= 30f)
        {
            return 4.5f;
        }
        else
        {
            return 5f;
        }
    }

    public void ChooseRandomNum()
    {
        //This function enables the spawner to randomize the spawn ratio of the Animal prefab as days increase.
        randomNum = Random.Range(1, 13);

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            if (randomNum >= 1 && randomNum <= 2)
            {
                SpawnAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            if (randomNum >= 1 && randomNum <= 3)
            {
                SpawnAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
        {
            if (randomNum >= 1 && randomNum <= 4)
            {
                SpawnAnimal();
            }
            else { }
        }

        //Debug.Log("Night time: " + NightTime.Instance.timeOfNight + " | Spawn interval: " + GetCurrentAnimalSpawnInterval() + " | Roll: " + randomNum);
    }

    public void SpawnAnimal()
    {
        //This function is resposnible for creating Animal prefab clones on dedicated spawn points.
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];
        GameObject newAnimal = Instantiate(animalPrefab, chosenSpawnPoint.position, Quaternion.identity, hutAnimalSpawn);
        activeAnimals++;

        ClickableTarget clickableTarget = newAnimal.GetComponent<ClickableTarget>();
        if (clickableTarget != null)
        {
            clickableTarget.SetAnimalSpawner(this);
        }
        else { }

            //This statement imports movement stages from the Animal behaviour.
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

    public void RegisterAnimalKilled()
    {
        activeAnimals--;


        if (activeAnimals < 0)
        {
            activeAnimals = 0;


        }
        else { }

        totalAnimalKills++;
        //Debug.Log("Animal killed. Total animal kills: " + totalAnimalKills);
    }

    public void RegisterAnimalDespawn()
    {
        //This function is responsible for removing the Animal prefab from these scene after some time.
        activeAnimals--;

        if (activeAnimals < 0)
        {
            activeAnimals = 0;
        }
        else { }
        //Debug.Log("Animal disappeard after 30 sec");
    }

    public void ClearAllAnimals()
    {
        //This function removes all Animal prefabs from the scene at a new day.
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
    }

    public void ResetAnimalTimer()
    {
        currentTimerRand = GetCurrentAnimalSpawnInterval();
    }
}