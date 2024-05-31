using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Missile _spawnedMissile;
    [SerializeField] private float _timerToSpawn = .5f;
    private bool _canSpawn;

    private void Start()
    {
        _canSpawn = true;
        StartCoroutine(SpawnMissiles());

    }
    IEnumerator SpawnMissiles()
    {
        yield return new WaitForSeconds(_timerToSpawn);
        if (_canSpawn ) 
            Instantiate(_spawnedMissile, _spawnPoints[Random.Range(0,_spawnPoints.Length)]);
        StartCoroutine(SpawnMissiles());
    }

    public void TurnSpawningOff()
    {
        _canSpawn = false;
    }
}
