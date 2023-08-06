using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController Instance { get; private set; }

    [SerializeField] private int currentCoins;

    [SerializeField] private CoinPickup coin;

    #region Properties
    public int CurrentCoins { get => currentCoins; set => currentCoins = value; }
    #endregion

    private void Awake() {
        Instance = this;
    }
    public void AddCoins(int coinsToAdd) {
        currentCoins += coinsToAdd;
        UIController.Instance.UpdateCoins(currentCoins);
    }

    public void DropCoin(Vector3 position, int value) {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        newCoin.CoinValue = value;
        newCoin.gameObject.SetActive(true);
    }

    public void SpendCoins(int coinsToSpend) {
        currentCoins -= coinsToSpend;
    }
}
