/*using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxAnimalOnScreem = 3;
    [SerializeField] private int activeAnimal = 0;
    [SerializeField] private int totalKills = 0;

    private float timer = 0f;
    private int maxAnimalOnScreen;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && activeAnimal < maxAnimalOnScreen)
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
        if (chosenSpawnPoint != null)
        {
            Debug.LogWarning("AnimalSpawner: One of the spawnpoints is null.");
        }
        GameObject newAnimal = Instantiate(animalPrefab, chosenSpawnPoint.position, Quaternion.identity);

        ClickableEnemy clickableEnemy = newAnimal.GetComponent<ClickableEnemy>();
        if (clickableEnemy != null)
        {
            clickableEnemy.SetSpawner(this);
        }
        else
        {
            Debug.LogWarning("MonsterSpawner: Spawned enemy does not have Clickable script.");
        }

        activeAnimal++;
        Debug.Log("Animal spawned. Active animal: " + activeAnimal);
    }
}
*/