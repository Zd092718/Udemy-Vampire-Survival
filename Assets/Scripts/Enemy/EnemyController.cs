using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damageDone;
    [SerializeField] private float hitWaitTime = 1f;
    [SerializeField] private float health = 5f;
    [SerializeField] private float knockbackTime = .5f;
    [SerializeField] private int enemyExpValue;
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float coinDropRate = .5f;
    private float _knockbackCounter;
    private float _hitCounter;
    private Rigidbody2D _rb;
    private Transform _target;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = PlayerHealthController.Instance.transform;
    }

    private void Update()
    {
        CheckKnockback();
        GiveChase();
        if (_hitCounter > 0f) {
            _hitCounter -= Time.deltaTime;
        }
        if (!PlayerController.Instance.gameObject.activeSelf)
        {
            _rb.velocity = Vector2.zero;
        }
            
    }

    private void CheckKnockback() {
        if (_knockbackCounter > 0) {
            _knockbackCounter -= Time.deltaTime;

            if (moveSpeed > 0) {
                moveSpeed = -moveSpeed * 2f;
            }

            if (_knockbackCounter <= 0f) {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
    }
    private void GiveChase() {
        _rb.velocity = (_target.position - transform.position).normalized * moveSpeed;
    }   

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && _hitCounter <= 0) {
            PlayerHealthController.Instance.TakeDamage(damageDone);
            _hitCounter = hitWaitTime;
        }
    }


    public void TakeDamage(float damage) {
        health -= damage;

        if(health <= 0f) {
            Destroy(gameObject);
            ExperienceLevelController.Instance.SpawnExp(transform.position, enemyExpValue);
            
            if(Random.value <= coinDropRate) {
                CoinController.Instance.DropCoin(transform.position, coinValue);
            }
        }

        DamageNumberController.Instance.SpawnDamage(damage, transform.position, Color.white);
    }

    public void TakeDamage(float damage, bool shouldKnockback) {
        TakeDamage(damage);
        if(shouldKnockback) {

            _knockbackCounter = knockbackTime;
        }
    }
}
