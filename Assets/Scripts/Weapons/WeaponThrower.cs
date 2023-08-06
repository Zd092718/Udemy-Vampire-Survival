using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{
   [SerializeField] private EnemyDamager damager;
   private float _throwCounter;

   private void Start()
   {
      SetStats();
   }
   
   private void Update()
   {
      if (StatsUpdated)
      {
         StatsUpdated = false;
            
         SetStats();
      }

      _throwCounter -= Time.deltaTime;
      if (_throwCounter <= 0f)
      {
         _throwCounter = Stats[WeaponLevel].timeBetweenAttacks;

         for (int i = 0; i < Stats[WeaponLevel].amount; i++)
         {
            Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
         }
      }
   }

   private void SetStats()
   {
      damager.Damage = Stats[WeaponLevel].damage;
      damager.LifeTime = Stats[WeaponLevel].duration;

      damager.transform.localScale = Vector3.one * Stats[WeaponLevel].range;

      _throwCounter = 0f;
   }
}
