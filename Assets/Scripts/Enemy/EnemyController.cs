using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damageDone;
    [SerializeField] private float hitWaitTime = 1f;
    private float hitCounter;
    private Rigidbody2D rb;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealthController.Instance.transform;
    }

    private void Update() {
        GiveChase();

        if(hitCounter > 0f) {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0) {
            PlayerHealthController.Instance.TakeDamage(damageDone);
            hitCounter = hitWaitTime;
        }
    }

    private void GiveChase() {
        rb.velocity = (target.position - transform.position).normalized;
    }
}
