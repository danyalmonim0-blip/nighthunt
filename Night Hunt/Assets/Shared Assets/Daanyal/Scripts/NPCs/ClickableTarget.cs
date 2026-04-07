using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    private MonsterSpawner monsterSpawner;
    private AnimalSpawner animalSpawner;
    private bool isDead = false;

    public void SetMonsterSpawner(MonsterSpawner newSpawner)
    {
        monsterSpawner = newSpawner;
        animalSpawner = null;
    }

    public void SetAnimalSpawner(AnimalSpawner newSpawner)
    {
        animalSpawner = newSpawner;
        monsterSpawner = null;
    }

    private void OnMouseDown()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (monsterSpawner != null)
        {
            monsterSpawner.RegisterEnemyKilled();
        }

        if (animalSpawner != null)
        {
            animalSpawner.RegisterAnimalKilled();
        }

        Destroy(gameObject);
    }
}