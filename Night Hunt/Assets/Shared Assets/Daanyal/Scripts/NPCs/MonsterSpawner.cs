using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform forestMonsterSpawn;

    //[SerializeField] private int maxEnemiesOnScreen = 2;
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

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            if (randomNum == 1 || randomNum == 2)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            if (randomNum == 1 || randomNum == 2 || randomNum == 3)
            {
                SpawnEnemy();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
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

        newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity, forestMonsterSpawn);
        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();

        //newEnemy.GetComponent<GameObject>();

        

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
        Debug.Log("Enemy killed. Total kills: " + totalKills);
    }
}