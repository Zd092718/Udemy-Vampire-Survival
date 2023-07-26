using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private float timeToSpawn;
    [SerializeField] private Transform minSpawn, maxSpawn;
    [SerializeField] private int checkPerFrame;
    [SerializeField] private GameObject enemyPool;
    [SerializeField] private List<WaveInfo> waves = new List<WaveInfo>();
    private int _currentWave;
    private float _waveCounter;
    private int _enemyToCheck;
    private float _spawnCounter;
    private Transform _target;
    private float _despawnDistance;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    void Start() {
        //spawnCounter = timeToSpawn;

        _target = PlayerHealthController.Instance.transform;

        _despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;

        _currentWave = -1;
        GoToNextWave();
    }

    void Update() {
        //spawnCounter -= Time.deltaTime;

        //if(spawnCounter < 0) {
        //    spawnCounter = timeToSpawn;
        //    //Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        //    GameObject newEnemy = Instantiate(enemyPrefab, SelectSpawnPoint(), Quaternion.identity, enemyPool.transform);
        //    spawnedEnemies.Add(newEnemy);
        //}

        transform.position = _target.position;

        if (PlayerHealthController.Instance.gameObject.activeSelf) {
            if (_currentWave < waves.Count) {
                _waveCounter -= Time.deltaTime;
                if (_waveCounter <= 0) {
                    GoToNextWave();
                }
                _spawnCounter -= Time.deltaTime;
                if (_spawnCounter <= 0) {
                    _spawnCounter = waves[_currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[_currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity, enemyPool.transform);
                    _spawnedEnemies.Add(newEnemy);
                }
            }
        }

        int checkTarget = _enemyToCheck + checkPerFrame;
        while (_enemyToCheck < checkTarget) {
            if (_enemyToCheck < _spawnedEnemies.Count) {
                if (_spawnedEnemies[_enemyToCheck] != null) {
                    if (Vector3.Distance(transform.position, _spawnedEnemies[_enemyToCheck].transform.position) > _despawnDistance) {
                        Destroy(_spawnedEnemies[_enemyToCheck]);
                        _spawnedEnemies.RemoveAt(_enemyToCheck);
                        checkTarget--;
                    } else {
                        _enemyToCheck++;
                    }
                } else {
                    _spawnedEnemies.RemoveAt(_enemyToCheck);
                    checkTarget--;
                }
            } else {
                _enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    private Vector3 SelectSpawnPoint() {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge) {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > 0.5f) {
                spawnPoint.x = maxSpawn.position.x;
            } else {
                spawnPoint.x = minSpawn.position.x;
            }
        } else {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > 0.5f) {
                spawnPoint.y = maxSpawn.position.y;
            } else {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    public void GoToNextWave() {
        _currentWave++;
        if (_currentWave >= waves.Count) {
            _currentWave = waves.Count - 1;
        }
        _waveCounter = waves[_currentWave].waveLength;
        _spawnCounter = waves[_currentWave].timeBetweenSpawns;
    }
}

[System.Serializable]
public class WaveInfo {
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
