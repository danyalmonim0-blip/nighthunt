using UnityEngine;

public class MonsterColor : MonoBehaviour
{
    public GameObject MonsterPrefab;

    public static MonsterColor Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    public void MonsterClr()
    {
        GameObject monsterPref = GameObject.Find("SampsaEnemy1_1");
        //monsterPref.GetComponent<Renderer>().material.color = new Color32(18, 255, 0, 255);
        //monsterPref.GetComponent<Renderer>().material.color = Color.green;
    }
}
