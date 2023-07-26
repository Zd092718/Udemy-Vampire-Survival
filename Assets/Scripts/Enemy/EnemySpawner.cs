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
    private int currentWave;
    private float waveCounter;
    private int enemyToCheck;
    private float spawnCounter;
    private Transform target;
    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start() {
        //spawnCounter = timeToSpawn;

        target = PlayerHealthController.Instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;

        currentWave = -1;
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

        transform.position = target.position;

        if (PlayerHealthController.Instance.gameObject.activeSelf) {
            if (currentWave < waves.Count) {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0) {
                    GoToNextWave();
                }
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0) {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity, enemyPool.transform);
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        int checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget) {
            if (enemyToCheck < spawnedEnemies.Count) {
                if (spawnedEnemies[enemyToCheck] != null) {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance) {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    } else {
                        enemyToCheck++;
                    }
                } else {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            } else {
                enemyToCheck = 0;
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
        currentWave++;
        if (currentWave >= waves.Count) {
            currentWave = waves.Count - 1;
        }
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }
}

[System.Serializable]
public class WaveInfo {
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
