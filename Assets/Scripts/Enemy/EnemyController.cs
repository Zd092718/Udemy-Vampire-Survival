using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update() {
        rb.velocity = (target.position - transform.position).normalized;
    }
}
