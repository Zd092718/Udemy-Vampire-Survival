using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour {
    [SerializeField] private TMP_Text upgradeDescText, nameLevelText;
    [SerializeField] private Image weaponIcon;

    private Weapon _mAssignedWeapon;

    public void UpdateButtonDisplay(Weapon weapon) {
        if (weapon.gameObject.activeSelf) {
            upgradeDescText.text = weapon.Stats[weapon.WeaponLevel].upgradeText;

            nameLevelText.text = weapon.name + " - Lvl " + weapon.WeaponLevel;
        } else {
            upgradeDescText.text = "Unlock " + weapon.name;
            nameLevelText.text = weapon.name;
        }

        weaponIcon.sprite = weapon.Icon;
        _mAssignedWeapon = weapon;
    }

    public void SelectUpgrade() {
        if (_mAssignedWeapon != null) {
            if (_mAssignedWeapon.gameObject.activeSelf) {
                _mAssignedWeapon.LevelUp();
            } else {
                PlayerController.Instance.AddWeapon(_mAssignedWeapon);
            }
            UIController.Instance.LevelUpPanel.SetActive(false);
            UIController.Instance.IsLevelingUp = false;
            Time.timeScale = 1.0f;
        }
    }
}
