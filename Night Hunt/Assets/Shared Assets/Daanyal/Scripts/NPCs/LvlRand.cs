using UnityEngine;

public class LvlRand : MonoBehaviour
{

    [SerializeField] private int lvlRandNum = 0;

    public void lvlRandomNum()
    {

    }

    void Start()
    {
        if (lvlRandNum == 0)
        {
            //MonsterSpawner.Instance.SpawnEnemy();
        }
    }

    void Update()
    {
        if (DayCounterManager.Instance.dayCounterManagerValues.dayCounter > 5)
        {
            lvlRandNum++;
        }
    }
}
