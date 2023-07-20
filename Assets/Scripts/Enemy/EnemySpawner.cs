using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private float timeToSpawn;
    private float spawnCounter;

    void Start()
    {
        spawnCounter = timeToSpawn;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        int randomEnemy = Random.Range(0, enemies.Count);
        if(spawnCounter < 0) {
            spawnCounter = timeToSpawn;
            Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        }
    }
}
