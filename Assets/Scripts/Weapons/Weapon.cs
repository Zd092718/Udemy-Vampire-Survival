using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [field: SerializeField] private List<WeaponStats> stats;
    [field: SerializeField] private int weaponLevel;
    [field: SerializeField] private Sprite icon;
    protected bool statsUpdated;

    public int WeaponLevel { get => weaponLevel; set => weaponLevel = value; }
    public List<WeaponStats> Stats { get => stats; set => stats = value; }
    public Sprite Icon { get => icon; set => icon = value; }

    public void LevelUp() {
        if(weaponLevel < stats.Count - 1) {
            weaponLevel++;
            statsUpdated = true;
            if(weaponLevel >= stats.Count - 1) {
                PlayerController.Instance.FullyLeveledWeapons.Add(this);
                PlayerController.Instance.AssignedWeapons.Remove(this);
            }
        }
    }
}
[System.Serializable]
public class WeaponStats {
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    public string upgradeText;
}
