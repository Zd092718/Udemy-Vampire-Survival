using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float spawnTime, spawnCounter;

    void Start()
    {
        SetStats();
    }


    void Update()
    {
        if (statsUpdated) {
            statsUpdated = false;  

            SetStats();
        }
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    private void SetStats()
    {
        damager.Damage = Stats[WeaponLevel].damage;
        damager.LifeTime = Stats[WeaponLevel].duration;
        damager.TimeBetweenDamage = Stats[WeaponLevel].speed;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].range;
        spawnTime = Stats[WeaponLevel].timeBetweenAttacks;
        spawnCounter = 0f;
    }
}
