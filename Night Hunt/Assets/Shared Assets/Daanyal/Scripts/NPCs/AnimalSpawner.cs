using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform hutAnimalSpawn;

    [SerializeField] private int maxAnimalOnScreen = 2;
    [SerializeField] private int activeAnimals = 0;
    [SerializeField] public int totalAnimalKills = 0;
    [SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;

    

    //public Transform[] stage1SpotsAn;
    //public Transform[] stage2SpotsAn;
    //public Transform[] stage3SpotsAn;

    //public GameObject hutAnimalSpawn;

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

        if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 0 && DayCounterManager.Instance.dayCounterManagerValues.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 3 && DayCounterManager.Instance.dayCounterManagerValues.dayCounter <= 6)
        {
            if (randomNum == 1 || randomNum == 2)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 7 && DayCounterManager.Instance.dayCounterManagerValues.dayCounter <= 10)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3)
            {
                SpawnAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 11)
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


        //var enemyInstance = /*GameObject newEnemy =*/ Instantiate(enemyPrefab, chosenSpawnPoint.transform.position, Quaternion.identity);
        //enemyInstance.transform.parent = transform;

        ClickableTarget clickableTarget = newAnimal.GetComponent<ClickableTarget>();

        //EnemyBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBehaviour>();



        activeAnimals++;

        Debug.Log("Animal spawned. Active Animals: " + activeAnimals);

        if (clickableTarget != null)
        {
            clickableTarget.SetAnimalSpawner(this);
        }
        //EnemyBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBehaviour>();
        //if (enemyBehaviour != null)
        //{
        //    enemyBehaviour.spawnPoint = chosenSpawnPoint;
        //    enemyBehaviour.stage1Spots = stage1Spots;
        //    enemyBehaviour.stage2Spots = stage2Spots;
        //    enemyBehaviour.stage3Spots = stage3Spots;
        }

    //public void AnimalKilledCnt()
    //{
    //    activeAnimals--;

    //    if (activeAnimals < 0)
    //    {
    //        activeAnimals = 0;
    //    }

    //    totalKills++;
    //    Debug.Log("Enemy killed. Total kills: " + totalKills);
    //}

    public void RegisterAnimalKilled()
    {
        activeAnimals--;

        if (activeAnimals < 0)
        {
            activeAnimals = 0;
        }

        totalAnimalKills++;
        //Debug.Log("Animal killed. Total animal kills: " + totalAnimalKills);


    }
}















/*
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxAnimalsOnScreen = 2;
    [SerializeField] private int activeAnimals = 0;
    [SerializeField] private int totalAnimalKills = 0;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && activeAnimals < maxAnimalsOnScreen)
        {
            timer = 0f;
            SpawnAnimal();
        }
    }

    private void SpawnAnimal()
    {
        if (animalPrefab == null)
        {
            Debug.LogWarning("AnimalSpawner: Animal prefab is missing.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("AnimalSpawner: No spawn points assigned.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        if (chosenSpawnPoint == null)
        {
            Debug.LogWarning("AnimalSpawner: One of the spawn points is null.");
            return;
        }

        GameObject newAnimal = Instantiate(animalPrefab, chosenSpawnPoint.position, Quaternion.identity);

        ClickableTarget clickableTarget = newAnimal.GetComponent<ClickableTarget>();

        if (clickableTarget != null)
        {
            clickableTarget.SetAnimalSpawner(this);
        }
        else
        {
            Debug.LogWarning("AnimalSpawner: Spawned animal does not have ClickableTarget script.");
        }

        activeAnimals++;
        Debug.Log("Animal spawned. Active animals: " + activeAnimals);
    }

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
}
*/