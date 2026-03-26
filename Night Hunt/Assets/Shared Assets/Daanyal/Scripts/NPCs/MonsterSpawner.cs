using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private float spawnTimer;

    private void Update()
    {
        spawnTimer = Time.time;
        if (spawnTimer > 0)
        {
            spawnTimer = 0f;
            //SpawnWave();
        }
    }
    /*private void SpawnWave()
    {
        if (enemyPrefab == null)
        {

        }
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForseconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
    }*/
}

