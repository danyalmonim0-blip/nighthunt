using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform forestMonsterSpawn;

    [SerializeField] private int activeEnemies = 0;
    [SerializeField] public int totalKills = 0;
    [SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;
    private GameObject newEnemy;

    public Transform[] stage1Spots;
    public Transform[] stage2Spots;
    public Transform[] stage3Spots;

    private Sprite enemyColor;


    public float randomNum;
    
    //private float timer = 0f;

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
        currentTimerRand = GetCurrentSpawnInterval();
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
                currentTimerRand = GetCurrentSpawnInterval();
            }
        }
    }

    public float GetCurrentSpawnInterval()
    {
        // Later in the night = faster spawn checks
        if (NightTime.Instance.timeOfNight >= 90f)
        {
            return 1.5f;
        }
        else if (NightTime.Instance.timeOfNight >= 60f)
        {
            return 2f;
        }
        else if (NightTime.Instance.timeOfNight >= 30f)
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

        currentTimerRand = timerRand;

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            //Debug.Log("spawn ratio 1");

            if (randomNum == 1)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            //Debug.Log("spawn ratio 1 and 2");

            if (randomNum >= 1 && randomNum <= 2)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            //Debug.Log("spawn ratio 1 to 3");

            if (randomNum >= 1 && randomNum <= 3)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
        {
            //Debug.Log("spawn ratio 1 to 4");

            if (randomNum >= 1 && randomNum <= 4)
            {
                SpawnEnemy();
            }
        }
        //Debug.Log("Night time: " + NightTime.Instance.timeOfNight + " | Spawn interval: " + GetCurrentSpawnInterval() + " | Roll: " + randomNum);
    }


    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity, forestMonsterSpawn);
        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();

        //newEnemy.GetComponent<GameObject>();

        

        activeEnemies++;
        Debug.Log("MONSTER spawned. Active MONSTERS: " + activeEnemies);

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

    public void SpawnPenaltyMonster()
{
    Debug.Log("PLAYER SHOT AN ANIMAL - penalty monster spawned.");
    SpawnEnemy();
}
    public void ChangeNPCColorGre()
    {
        if (activeEnemies > 0)
        {
            SpriteRenderer EnemyCol = newEnemy.GetComponent<SpriteRenderer>();
            EnemyCol.color = Color.green;
        }
    }

    public void ChangeNPCColorBla()
    {
        if (activeEnemies > 0)
        {
            SpriteRenderer EnemyCol = newEnemy.GetComponent<SpriteRenderer>();
            EnemyCol.color = Color.black;
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
        Debug.Log("MONSTER killed. Total kills: " + totalKills);
    }

    public void ClearAllMonsters()
    {
        if (forestMonsterSpawn == null)
        {
            return;
        }
        for (int i = forestMonsterSpawn.childCount - 1; i >= 0; i--)
        {
            Destroy(forestMonsterSpawn.GetChild(i).gameObject);
        }

        activeEnemies = 0;
        currentTimerRand = GetCurrentSpawnInterval();
        Debug.Log("All monsters cleared.");
    }

    public void ResetMonsterTimer()
    {
        currentTimerRand = GetCurrentSpawnInterval();
    }
}