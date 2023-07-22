using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {
    [SerializeField] private float damage = 5f;
    [SerializeField] private float growSpeed = 5f;
    [SerializeField] private float lifeTime;
    [SerializeField] private bool shouldKnockback;
    [SerializeField] private bool destroyParent;
    private Vector3 targetSize;

    public float Damage { get => damage; set => damage = value; }
    public float GrowSpeed { get => growSpeed; set => growSpeed = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    private void Start() {

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

    }

    private void Update() {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        DestroyAfterLifetime();
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
        if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
            enemy.TakeDamage(damage, shouldKnockback);
        }
    }
}
