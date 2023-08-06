using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] private float throwPower;
    [SerializeField] private float rotateSpeed;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(rb.velocity.x)));
    }
}
