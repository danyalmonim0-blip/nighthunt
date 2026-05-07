using UnityEngine;

public class ClickableTarget : MonoBehaviour
{
    private MonsterSpawner monsterSpawner;
    private AnimalSpawner animalSpawner;
    public bool isDead = false;
    private Animator enemymator;

    private AudioSource enemySource;

    public AudioClip appearSound;
    public AudioClip gunShotSound;

    private void Start()
    {
        enemymator = GetComponent<Animator>();

        enemySource = GetComponent<AudioSource>();

        enemySource.PlayOneShot(appearSound);
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

    public void GetShot()
    {
        enemySource.PlayOneShot(gunShotSound);

        //This function is responsible for killing the Monsters and Animals.
        if (Gun.Instance.isGunEmpty == false)
        {
            if (isDead)
            {
                return;
            }
            else { }

            isDead = true;

            if (monsterSpawner != null)
            {
                monsterSpawner.MonsterKilledCnt();
            }
            else { }

            if (animalSpawner != null)
            {
                animalSpawner.RegisterAnimalKilled();

                AnimalBehaviour animalBehaviour = GetComponent<AnimalBehaviour>();
                if (animalBehaviour != null)
                {
                    animalBehaviour.MarkAsRemoved();
                }
                else { }

                if (MonsterSpawner.Instance != null)
                {
                    MonsterSpawner.Instance.SpawnPenaltyMonster();
                }
                else { }
            }

            if (enemymator != null)
            {
                enemymator.SetTrigger("Died");
            }
            else { }

            Invoke("MakeItDestroyed", 2.5f);

            Gun.Instance.currentBullets--;
            //Debug.Log(Gun.Instance.currentBullets);
        }
    }

    void MakeItDestroyed()
    {
        Destroy(gameObject);
    }
}