using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform holder, fireballToSpawn;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private EnemyDamager damager;
    private float spawnCounter;

    private void Start() {
        SetStats();

        //UIController.Instance.LevelUpButtons[0].UpdateButtonDisplay(this);
    }

    void Update()
    {
        //holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * Stats[WeaponLevel].speed));

        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0) {
            spawnCounter = timeBetweenSpawn;

            Transform spawnedFireball = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder);
            spawnedFireball.gameObject.SetActive(true);
        }
        if (statsUpdated) {
            statsUpdated = false;  

            SetStats();
        }
    }

    public void SetStats() {
        damager.Damage = Stats[WeaponLevel].damage;

        transform.localScale = Vector3.one * Stats[WeaponLevel].range;

        timeBetweenSpawn = Stats[WeaponLevel].timeBetweenAttacks;

        damager.LifeTime = Stats[WeaponLevel].duration;

        spawnCounter = 0f;
    }
    
}
