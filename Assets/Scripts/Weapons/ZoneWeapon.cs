using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    private float _spawnTime, _spawnCounter;

    void Start()
    {
        SetStats();
    }


    void Update()
    {
        if (StatsUpdated) {
            StatsUpdated = false;  

            SetStats();
        }
        _spawnCounter -= Time.deltaTime;
        if (_spawnCounter <= 0f)
        {
            _spawnCounter = _spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    private void SetStats()
    {
        damager.Damage = Stats[WeaponLevel].damage;
        damager.LifeTime = Stats[WeaponLevel].duration;
        damager.TimeBetweenDamage = Stats[WeaponLevel].speed;
        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].range;
        _spawnTime = Stats[WeaponLevel].timeBetweenAttacks;
        _spawnCounter = 0f;
    }
}
