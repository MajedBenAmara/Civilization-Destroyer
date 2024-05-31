using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public float MinSpawnTime = .5f, MaxSpawnTime = 1f;
    public GameObject MissileGameObject;

    private bool _canSpawn;

    private void Start()
    {
        _canSpawn = true;
        StartCoroutine(SpawnMissile());
    }

    IEnumerator SpawnMissile()
    {
        yield return new WaitForSeconds(Random.Range(MinSpawnTime, MaxSpawnTime));
        if(_canSpawn)
            Instantiate(MissileGameObject, transform);
        StartCoroutine(SpawnMissile());
    }

    public void TurnSpawnOff()
    {
        _canSpawn = false;

    }
}
