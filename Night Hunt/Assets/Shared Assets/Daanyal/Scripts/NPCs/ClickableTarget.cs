using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    private MonsterSpawner monsterSpawner;
    private AnimalSpawner animalSpawner;
    private bool isDead = false;
    private Animator enemymator;

    private AudioSource EnnemySource;

    //public AudioClip AppearS;
    //public AudioClip GunshotS;

    private void Start()
    {
        enemymator = GetComponent<Animator>();

        EnnemySource = GetComponent<AudioSource>();

        //EnnemySource.PlayOneShot(AppearS);
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
        //EnnemySource.PlayOneShot(GunshotS);

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

            AnimalBehaviour animalBehaviour = GetComponent<AnimalBehaviour>();
            if (animalBehaviour != null)
            {
                animalBehaviour.MarkAsRemoved();
            }

        }

        if (enemymator != null)
        {
            enemymator.SetTrigger("Died");
        }

        Invoke("MakeItDestroyed", 2.5f);

        /*if (gameState.Instance.currentTime == 1)
        {
            Destroy(gameObject);
        }*/
    }

    void MakeItDestroyed()
    {
        Destroy(gameObject);

    }
}