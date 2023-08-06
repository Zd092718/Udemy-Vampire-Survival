using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenChecks = .2f;
    private float _checkCounter;
    private bool _movingToPlayer;
    private PlayerController _player;

    public int CoinValue {
        get {
            return coinValue;
        }
        set {
            coinValue = value;
        }
    }

    private void Start() {
        _player = PlayerController.Instance;
    }

    private void Update() {
        if (_movingToPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, moveSpeed * Time.deltaTime);
        } else {
            _checkCounter -= Time.deltaTime;
            if (_checkCounter <= 0) {
                _checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, _player.transform.position) < _player.PickupRange) {
                    _movingToPlayer = true;
                    moveSpeed += _player.MoveSpeed;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            CoinController.Instance.AddCoins(CoinValue);

            Destroy(this.gameObject);
        }
    }
}
