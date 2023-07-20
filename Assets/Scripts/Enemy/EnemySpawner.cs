using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Transform minSpawn, maxSpawn;
    private float spawnCounter;
    private Transform target;

    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.Instance.transform;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        int randomEnemy = Random.Range(0, enemies.Count);
        if(spawnCounter < 0) {
            spawnCounter = timeToSpawn;
            //Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
            Instantiate(enemies[randomEnemy], SelectSpawnPoint(), Quaternion.identity);
        }
        transform.position = target.position;
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
