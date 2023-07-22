using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescText, nameLevelText;
    [SerializeField] private Image weaponIcon;

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon weapon) {
        upgradeDescText.text = weapon.Stats[weapon.WeaponLevel].upgradeText;
        weaponIcon.sprite = weapon.Icon;

        nameLevelText.text = weapon.name + " - Lvl " + weapon.WeaponLevel;

        assignedWeapon = weapon; 
    }

    public void SelectUpgrade() {
        if(assignedWeapon != null) {
            assignedWeapon.LevelUp();
            UIController.Instance.LevelUpPanel.SetActive(false);
            UIController.Instance.IsLevelingUp = false;
            Time.timeScale = 1.0f;
        }
    }
}
