using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    //[SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemiesOnScreen = 2;
    [SerializeField] private int activeEnemies = 0;
    [SerializeField] private int totalKills = 0;
    [SerializeField] private float timerRand = 3f;
    [SerializeField] private float currentTimerRand;


    private float randomNum;
    
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

        timerRand -= 1 * Time.deltaTime;
        if (timerRand <= 0)
        {
            RandomNum();
            currentTimerRand = timerRand;
        }

        if (gameState.Instance.dayCounter >= 0 && gameState.Instance.dayCounter <= 4)
        {
            Debug.Log("lol");

            if (randomNum == 1)
            {
                SpawnEnemy();
            }
        }
        
        if (gameState.Instance.dayCounter >= 5 && gameState.Instance.dayCounter <= 9)
        {
            if (randomNum == 1 || randomNum == 2)
            {
                SpawnEnemy();
            }
        }
    }

    public void RandomNum()
    {
        randomNum = Random.Range(1, 7);
        Debug.Log(randomNum);
    }

    

    private void Update()
    {

    }

    public void SpawnEnemy()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity);

        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();
        
        activeEnemies++;

        Debug.Log("Enemy spawned. Active enemies: " + activeEnemies);

        if (clickableTarget != null)
        {
            clickableTarget.SetMonsterSpawner(this);
        }


        if (enemyPrefab == null)
        {
            Debug.LogWarning("MonsterSpawner: Enemy prefab is missing.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("MonsterSpawner: No spawn points assigned.");
            return;
        }

        if (chosenSpawnPoint == null)
        {
            Debug.LogWarning("MonsterSpawner: One of the spawn points is null.");
            return;
        }

        else
        {
            Debug.LogWarning("MonsterSpawner: Spawned enemy does not have ClickableTarget script.");
        }
    }

    public void RegisterEnemyKilled()
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