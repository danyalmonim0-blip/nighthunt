using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    private MonsterSpawner monsterSpawner;
    private AnimalSpawner animalSpawner;
    private bool isDead = false;
    private Animator enemymator;

    private void Start()
    {
        enemymator = GetComponent<Animator>();
    }

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
            monsterSpawner.MonsterKilledCnt();
        }

        if (animalSpawner != null)
        {
            animalSpawner.RegisterAnimalKilled();
        }

        enemymator.SetTrigger("Died");

        Invoke("MakeItDestroyed", 2.5f);

        if (gameState.Instance.currentTime == 1)
        {
            Destroy(gameObject);
        }
    }

    void MakeItDestroyed()
    {
        Destroy(gameObject);

    }
}