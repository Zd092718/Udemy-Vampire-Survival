using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    [SerializeField] private EnemyDamager damager;

    private float _attackCounter, _direction;
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

        _attackCounter -= Time.deltaTime;
        if (_attackCounter <= 0)
        {
            _attackCounter = Stats[WeaponLevel].timeBetweenAttacks;

            _direction = Input.GetAxisRaw("Horizontal");

            if (_direction != 0)
            {
                if (_direction > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
                
            }
            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            
            for(int i = 1; i < Stats[WeaponLevel].amount; i++) {
                float rot = (360f / Stats[WeaponLevel].amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }
        }
    }

    private void SetStats()
    {
        damager.Damage = Stats[WeaponLevel].damage;
        damager.LifeTime = Stats[WeaponLevel].duration;

        damager.transform.localScale = Vector3.one * Stats[WeaponLevel].range;

        _attackCounter = 0f;
    }
}
