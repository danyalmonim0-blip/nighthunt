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