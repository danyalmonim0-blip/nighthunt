using Unity.VisualScripting;
using UnityEngine;

public class ClickableEnemy : MonoBehaviour
{
    private monsterSpawner spawner;
    private bool isDead = false;

    public void SetSpawner(monsterSpawner newSpawner)
    {
        spawner = newSpawner;
    }

    private void OnMouseDown()
    {
        if (isDead)
        {
            return;
        }
        (isDead) = true;

        if (spawner != null)
        {
            spawner.RegisterEnemyKilled();
        }
        Destroy(gameObject);
    }
}
