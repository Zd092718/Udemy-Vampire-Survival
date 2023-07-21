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
    private float knockbackCounter;
    private float hitCounter;
    private Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealthController.Instance.transform;
    }

    private void Update() {
        CheckKnockback();
        GiveChase();
        if (hitCounter > 0f) {
            hitCounter -= Time.deltaTime;
        }
    }

    private void CheckKnockback() {
        if (knockbackCounter > 0) {
            knockbackCounter -= Time.deltaTime;

            if (moveSpeed > 0) {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockbackCounter <= 0f) {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
    }
    private void GiveChase() {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
    }   

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0) {
            PlayerHealthController.Instance.TakeDamage(damageDone);
            hitCounter = hitWaitTime;
        }
    }


    public void TakeDamage(float damage) {
        health -= damage;

        if(health <= 0f) {
            Destroy(gameObject);
        }

        DamageNumberController.Instance.SpawnDamage(damage, transform.position, Color.white);
    }

    public void TakeDamage(float damage, bool shouldKnockback) {
        TakeDamage(damage);
        if(shouldKnockback) {

            knockbackCounter = knockbackTime;
        }
    }
}
