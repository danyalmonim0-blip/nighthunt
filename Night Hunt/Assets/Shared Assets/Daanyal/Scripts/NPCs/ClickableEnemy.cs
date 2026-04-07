using UnityEngine;

public class ClickableEnemy : MonoBehaviour
{
    private MonsterSpawner spawner;
    private bool isDead = false;

    public void SetSpawner(MonsterSpawner newSpawner)
    {
        spawner = newSpawner;
    }

    private void OnMouseDown()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        if (spawner != null)
        {
            spawner.RegisterEnemyKilled();
        }

        Destroy(gameObject);
    }
}