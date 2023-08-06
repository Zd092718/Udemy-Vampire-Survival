using System.Collections.Generic;
using UnityEngine;


public class ExperienceLevelController : MonoBehaviour {
    public static ExperienceLevelController Instance { get; private set; }

    [SerializeField] private int currentExperience;
    [SerializeField] private List<int> expLevels;
    [SerializeField] private int currentLevel = 1, levelCount = 100;
    [SerializeField] private float xpPickupDespawnTime;
    [SerializeField] private List<Weapon> weaponsToUpgrade;
    [SerializeField] private ExpPickup expPickup;
    [SerializeField] private Transform pickupPool;
    [SerializeField] private PlayerStatController playerStatController;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There's already an instance of ExperienceLevelController");
        }
        Instance = this;
    }

    private void Start() {
        while (expLevels.Count < levelCount) {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet) {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel]) {
            LevelUp();
        }

        UIController.Instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue) {
        ExpPickup xpDrop = Instantiate(expPickup, position, Quaternion.identity, pickupPool);
        xpDrop.ExpValue = expValue;
        Destroy(xpDrop.gameObject, xpPickupDespawnTime);
    }

    public void LevelUp() {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if (currentLevel >= expLevels.Count) {
            currentLevel = expLevels.Count - 1;
        }

        ActivateLevelUpPanel();

        //UIController.Instance.LevelUpButtons[0].UpdateButtonDisplay(PlayerController.Instance.AssignedWeapons[0]);
        //UIController.Instance.LevelUpButtons[1].UpdateButtonDisplay(PlayerController.Instance.UnassignedWeapons[0]);
        //UIController.Instance.LevelUpButtons[2].UpdateButtonDisplay(PlayerController.Instance.UnassignedWeapons[1]);

        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.Instance.AssignedWeapons);

        if (availableWeapons.Count > 0) {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.Instance.AssignedWeapons.Count + PlayerController.Instance.FullyLeveledWeapons.Count < PlayerController.Instance.MaxWeapons) {
            availableWeapons.AddRange(PlayerController.Instance.UnassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < PlayerController.Instance.MaxWeapons; i++) {
            if (availableWeapons.Count > 0) {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++) {
            UIController.Instance.LevelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.Instance.LevelUpButtons.Length; i++) {
            if (i < weaponsToUpgrade.Count) {
                UIController.Instance.LevelUpButtons[i].gameObject.SetActive(true);
            } else {
                UIController.Instance.LevelUpButtons[i].gameObject.SetActive(false);
            }
        }

        playerStatController.UpdateDisplay();
    }

    private static void ActivateLevelUpPanel() {
        // Pull up level up screen and disable main menu controls
        UIController.Instance.LevelUpPanel.SetActive(true);
        UIController.Instance.SetLevelUpSelected();
        UIController.Instance.IsLevelingUp = true;
        Time.timeScale = 0f;
    }

}
