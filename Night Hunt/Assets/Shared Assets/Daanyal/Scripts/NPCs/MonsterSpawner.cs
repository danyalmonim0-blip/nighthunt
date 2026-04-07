using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemiesOnScreen = 3;
    [SerializeField] private int activeEnemies = 0;
    [SerializeField] private int totalKills = 0;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && activeEnemies < maxEnemiesOnScreen)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
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

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        if (chosenSpawnPoint == null)
        {
            Debug.LogWarning("MonsterSpawner: One of the spawn points is null.");
            return;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity);

        ClickableTarget clickableTarget = newEnemy.GetComponent<ClickableTarget>();

        if (clickableTarget != null)
        {
            clickableTarget.SetMonsterSpawner(this);
        }
        else
        {
            Debug.LogWarning("MonsterSpawner: Spawned enemy does not have ClickableTarget script.");
        }

        activeEnemies++;
        Debug.Log("Enemy spawned. Active enemies: " + activeEnemies);
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