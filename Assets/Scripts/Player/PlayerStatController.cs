using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour {
    [SerializeField] private List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    [SerializeField] private int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    [SerializeField] private int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    #region Properties
    public List<PlayerStatValue> MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public List<PlayerStatValue> Health { get => health; set => health = value; }
    public List<PlayerStatValue> PickupRange { get => pickupRange; set => pickupRange = value; }
    public List<PlayerStatValue> MaxWeapons { get => maxWeapons; set => maxWeapons = value; }

    #endregion
    private void Start() {
        // Adding new stat values based on previous values and max level count
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++) {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++) {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++) {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    private void Update() {
        if (UIController.Instance.LevelUpPanel.activeSelf) {
            UpdateDisplay();
        }
    }

    public void UpdateDisplay() {
        if (moveSpeedLevel < moveSpeed.Count - 1) {
            UIController.Instance.MoveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        } else {
            UIController.Instance.MoveSpeedUpgradeDisplay.ShowMaxLevel();
        }
        if (healthLevel < health.Count - 1) {
            UIController.Instance.HealthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        } else {
            UIController.Instance.HealthUpgradeDisplay.ShowMaxLevel();
        }
        if (pickupRangeLevel < pickupRange.Count - 1) {
            UIController.Instance.PickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        } else {
            UIController.Instance.PickupRangeUpgradeDisplay.ShowMaxLevel();
        }
        if (maxWeaponsLevel < maxWeapons.Count - 1) {
            UIController.Instance.MaxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        } else {
            UIController.Instance.MaxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed() {
        moveSpeedLevel++;
        CoinController.Instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        PlayerController.Instance.MoveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void PurchaseHealth() {
        healthLevel++;
        CoinController.Instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        PlayerHealthController.Instance.MaxHealth = health[healthLevel].value;
        PlayerHealthController.Instance.CurrentHealth += health[healthLevel].value - health[healthLevel - 1].value;
    }

    public void PurchasePickupRange() {
        pickupRangeLevel++;
        CoinController.Instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();

        PlayerController.Instance.PickupRange = pickupRange[pickupRangeLevel].value;
    }

    public void PurchaseMaxWeapons() {
        maxWeaponsLevel++;
        CoinController.Instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateDisplay();

        PlayerController.Instance.MaxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
    }
}

[System.Serializable]
public class PlayerStatValue {
    public int cost;
    public float value;

    public PlayerStatValue(int newcost, float newValue) {
        cost = newcost;
        value = newValue;
    }
}
