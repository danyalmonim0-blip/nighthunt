using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform hutMonsterSpawn;

    [SerializeField] private int maxEnemiesOnScreen = 2;
    [SerializeField] private int activeEnemies = 0;
    [SerializeField] public int totalKills = 0;
    [SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;

    public Transform[] stage1Spots;
    public Transform[] stage2Spots;
    public Transform[] stage3Spots;


    public float randomNum;
    
    private float timer = 0f;

    public static MonsterSpawner Instance;
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
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 3 && DayCounterManager.Instance.dayCounterManagerValues.dayCounter <= 6)
        {
            if (randomNum == 1 || randomNum == 2)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 7 && DayCounterManager.Instance.dayCounterManagerValues.dayCounter <= 10)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter >= 11)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3 || randomNum == 4)
            {
                SpawnEnemy();
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

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity, hutMonsterSpawn);
        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();

        activeEnemies++;
        Debug.Log("Enemy spawned. Active enemies: " + activeEnemies);

        if (clickableTarget != null)
        {
            clickableTarget.SetMonsterSpawner(this);
        }
        EnemyBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBehaviour>();
        if (enemyBehaviour != null)
        {
            enemyBehaviour.spawnPoint = chosenSpawnPoint;
            enemyBehaviour.stage1Spots = stage1Spots;
            enemyBehaviour.stage2Spots = stage2Spots;
            enemyBehaviour.stage3Spots = stage3Spots;
        }
    }

    public void MonsterKilledCnt()
    {
        activeEnemies--;

        if (activeEnemies < 0)
        {
            activeEnemies = 0;
        }

        totalKills++;
        Debug.Log("Enemy killed. Total kills: " + totalKills);
    }
}