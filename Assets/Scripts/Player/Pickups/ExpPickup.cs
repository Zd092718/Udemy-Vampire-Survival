using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class ExpPickup : MonoBehaviour
{
    [SerializeField] private int expValue = 1;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenChecks = .2f;
    private float _checkCounter;
    private bool _movingToPlayer;
    private PlayerController _player;

    public int ExpValue
    {
        get
        {
            return expValue;
        }
        set
        {
            expValue = value;
        }
    }

    private void Start() {
        _player = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    private void Update() {
        if(_movingToPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, moveSpeed * Time.deltaTime);
        } else {
            _checkCounter -= Time.deltaTime;
            if(_checkCounter <= 0 ) {
                _checkCounter = timeBetweenChecks;

                if(Vector3.Distance(transform.position, _player.transform.position) < _player.PickupRange ) {
                    _movingToPlayer = true;
                    moveSpeed += _player.MoveSpeed;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            ExperienceLevelController.Instance.GetExp(ExpValue);

            Destroy(this.gameObject);
        }
    }
}
