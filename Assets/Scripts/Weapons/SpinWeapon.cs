using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder, fireballToSpawn;
    [SerializeField] private float timeBetweenSpawn;
    private float spawnCounter;

    private void Start() {
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0) {
            spawnCounter = timeBetweenSpawn;

            Transform spawnedFireball = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder);
            spawnedFireball.gameObject.SetActive(true);
        }

    }

    
}
