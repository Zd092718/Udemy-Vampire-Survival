using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float weaponRange;
    [SerializeField] private LayerMask enemyMask;

    private float _shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsUpdated)
        {
            StatsUpdated = false;
            
            SetStats();
        }

        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0f)
        {
            _shotCounter = Stats[WeaponLevel].timeBetweenAttacks;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * Stats[WeaponLevel].range, enemyMask);
            if (enemies.Length > 0)
            {
                for (int i = 0; i < Stats[WeaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);

                }
            }
        }
    }

    private void SetStats()
    {
        damager.Damage = Stats[WeaponLevel].damage;
        damager.LifeTime = Stats[WeaponLevel].duration;

        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].range;

        _shotCounter = 0f;

        projectile.MoveSpeed = Stats[WeaponLevel].speed;
    }
}
