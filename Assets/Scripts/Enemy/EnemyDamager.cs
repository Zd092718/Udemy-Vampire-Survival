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
    [SerializeField] private bool destroyOnImpact;
    
    private float _damageCounter;
    private Vector3 _targetSize;
    private List<EnemyController> _enemiesInRange = new List<EnemyController>();

    #region Properties
    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public float GrowSpeed
    {
        get
        {
            return growSpeed;
        }
        set
        {
            growSpeed = value;
        }
    }
    public float LifeTime
    {
        get
        {
            return lifeTime;
        }
        set
        {
            lifeTime = value;
        }
    }
    public float TimeBetweenDamage
    {
        get
        {
            return timeBetweenDamage;
        }
        set
        {
            timeBetweenDamage = value;
        }
    }

    #endregion

    private void Start() {

        _targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

    }

    private void Update() {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        DestroyAfterLifetime();

        if (damageOverTime) {
            _damageCounter -= Time.deltaTime;

            if (_damageCounter <= 0) {
                _damageCounter = TimeBetweenDamage;
                for(int i = 0; i < _enemiesInRange.Count; i++) {
                    if (_enemiesInRange[i] != null) {
                        _enemiesInRange[i].TakeDamage(damage, shouldKnockback);
                    } else {
                        _enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public void DestroyAfterLifetime() {
        if (lifeTime <= 0) {
            _targetSize = Vector3.zero;
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
                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        } else {
            if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
                _enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (damageOverTime) {
            if (collision.TryGetComponent<EnemyController>(out EnemyController enemy)) {
                _enemiesInRange.Remove(enemy);
            }
        }
    }
}
