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

    // These are the movement spots where Monster prefab can move.
    public Transform[] stage1Spots;
    public Transform[] stage2Spots;
    public Transform[] stage3Spots;

    private Sprite enemyColor;

    public float randomNum;

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
        else { }

        if (UIElements.Instance.StartingTxtPanel.activeSelf == false)
        {
            currentTimerRand -= 1 * Time.deltaTime;
            if (currentTimerRand <= 0)
            {
                ChooseRandomNum();
                currentTimerRand = GetCurrentSpawnInterval();
            }
            else { }
        }
        else { }
    }

    public float GetCurrentSpawnInterval()
    {
        //This function is responsible for increasing the spawn ratio within the day as night time increases.
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

    public void ChooseRandomNum()
    {
        //This function enables the spawner to randomize the spawn ratio of the Monster prefab as days increase.
        randomNum = Random.Range(1, 13);
        currentTimerRand = timerRand;

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 2)
        {
            if (randomNum == 1)
            {
                SpawnEnemy();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 3 && DayCounterManager.Instance.dayCounter <= 6)
        {
            if (randomNum >= 1 && randomNum <= 2)
            {
                SpawnEnemy();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 7 && DayCounterManager.Instance.dayCounter <= 10)
        {
            if (randomNum >= 1 && randomNum <= 3)
            {
                SpawnEnemy();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 11)
        {
            if (randomNum >= 1 && randomNum <= 4)
            {
                SpawnEnemy();
            }
            else { }
        }
        else { }

        //Debug.Log("Night time: " + NightTime.Instance.timeOfNight + " | Spawn interval: " + GetCurrentSpawnInterval() + " | Roll: " + randomNum);
    }


    public void SpawnEnemy()
    {
        //This function is resposnible for creating Monster prefab clones on dedicated spawn points.
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity, forestMonsterSpawn);
        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();

        activeEnemies++;

        if (clickableTarget != null)
        {
            clickableTarget.SetMonsterSpawner(this);
        }
        else { }

        //This statement imports movement stages from the Monster behaviour.
        EnemyBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBehaviour>();
        if (enemyBehaviour != null)
        {
            enemyBehaviour.spawnPoint = chosenSpawnPoint;
            enemyBehaviour.stage1Spots = stage1Spots;
            enemyBehaviour.stage2Spots = stage2Spots;
            enemyBehaviour.stage3Spots = stage3Spots;
        }
        else { }
    }

    public void SpawnPenaltyMonster()
    {
         //This function is responsible for spawning a Monster independently when an Animal is killed.
         SpawnEnemy();
    }
    

    public void MonsterKilledCnt()
    {
        //This function is responsible for counting kills.
        activeEnemies--;

        if (activeEnemies < 0)
        {
            activeEnemies = 0;
        }
        else { }

            totalKills++;
    }

    public void ClearAllMonsters()
    {
        //This function removes all Monster prefabs from the scene at a new day.
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
    }

    public void ResetMonsterTimer()
    {
        currentTimerRand = GetCurrentSpawnInterval();
    }
}