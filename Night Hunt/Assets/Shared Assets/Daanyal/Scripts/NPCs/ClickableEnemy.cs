using UnityEngine;

public class ClickableEnemy : MonoBehaviour
{
    private monsterSpawner spawner;

    public void SetSpawner()
    {
        monsterSpawner monsterSpawner = null;
        spawner = monsterSpawner;
    }
    
   
private void OnMouseDown()
    {
        if (spawner != null)
        {
            spawner.registerKill();
        }
        Destroy(gameObject);
    }
}
