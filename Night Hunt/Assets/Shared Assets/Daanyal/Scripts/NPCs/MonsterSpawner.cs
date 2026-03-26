using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject MonsterPrefab;

    [SerializeField]
    private float MonsterInterval = 3.5f;
    private object enemy;

    private void Start()
    {
        StartCoroutine(MonsterSpawner(MonsterInterval, MonsterPrefab));
    }

    private IEnumerator MonsterSpawner(float interval, GameObject monster)
    {
        yield return new WaitForEndOfSeconds(interval);
        GameObject newMonster = Instantiate(monster, new Vector3(UnityEngine.Random.Range(-5f, 5), UnityEngine.Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnMonster(interval, enemy));
    }

    private string spawnMonster(float interval, object enemy)
    {
        throw new NotImplementedException();
    }

    internal void registerKill()
    {
        throw new NotImplementedException();
    }

    /*  internal void registerKill()
      {
          throw new NotImplementedException();
      }
    */
}

