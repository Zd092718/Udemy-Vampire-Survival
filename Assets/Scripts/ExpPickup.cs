using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    [SerializeField] private int expValue = 1;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenChecks = .2f;
    private float checkCounter;
    private bool movingToPlayer;
    private PlayerController player;

    public int ExpValue { get => expValue; set => expValue = value; }

    private void Start() {
        player = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    private void Update() {
        if(movingToPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } else {
            checkCounter -= Time.deltaTime;
            if(checkCounter <= 0 ) {
                checkCounter = timeBetweenChecks;

                if(Vector3.Distance(transform.position, player.transform.position) < player.PickupRange ) {
                    movingToPlayer = true;
                    moveSpeed += player.MoveSpeed;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            ExperienceLevelController.Instance.GetExp(ExpValue);

            Destroy(gameObject);
        }
    }
}
