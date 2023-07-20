using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Transform minSpawn, maxSpawn;
    [SerializeField] private int checkPerFrame;
    [SerializeField] private GameObject enemyPool;
    private int enemyToCheck;
    private float spawnCounter;
    private Transform target;
    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.Instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        int randomEnemy = Random.Range(0, enemies.Count);
        if(spawnCounter < 0) {
            spawnCounter = timeToSpawn;
            //Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
            GameObject newEnemy = Instantiate(enemies[randomEnemy], SelectSpawnPoint(), Quaternion.identity, enemyPool.transform);
            spawnedEnemies.Add(newEnemy);
        }
        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;
        while(enemyToCheck < checkTarget) {
            if(enemyToCheck < spawnedEnemies.Count) {
                if (spawnedEnemies[enemyToCheck] != null) {
                    if(Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance) {
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

        if(spawnVerticalEdge) {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            
            if(Random.Range(0f, 1f) > 0.5f) {
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
}
