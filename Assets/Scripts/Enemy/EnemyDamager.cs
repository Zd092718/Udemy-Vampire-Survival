using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private float damage = 5f;
    [SerializeField] private float growSpeed = 5f;
    [SerializeField] private float lifeTime;
    [SerializeField] private bool shouldKnockback;
    [SerializeField] private bool destroyParent;
    [SerializeField] private bool damageOverTime;
    [SerializeField] private float timeBetweenDamage;
    private float damageCounter;
    private Vector3 targetSize;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    #region Properties
    public float Damage { get => damage; set => damage = value; }
    public float GrowSpeed { get => growSpeed; set => growSpeed = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }
    public float TimeBetweenDamage { get => timeBetweenDamage; set => timeBetweenDamage = value; }
    #endregion

    private void Start() {

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

    }

    private void Update() {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        DestroyAfterLifetime();

        if (damageOverTime) {
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0) {
                damageCounter = TimeBetweenDamage;
                for(int i = 0; i < enemiesInRange.Count; i++) {
                    if (enemiesInRange[i] != null) {
                        enemiesInRange[i].TakeDamage(damage, shouldKnockback);
                    } else {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public void DestroyAfterLifetime() {
        if (lifeTime <= 0) {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f) {
                Destroy(gameObject);
                if (destroyParent) {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!damageOverTime) {
            if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
                enemy.TakeDamage(damage, shouldKnockback);
            }
        } else {
            if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (damageOverTime) {
            if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
