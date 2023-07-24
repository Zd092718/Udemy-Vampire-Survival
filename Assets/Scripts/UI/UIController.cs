using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("Level Settings & References")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private LevelUpSelectionButton[] levelUpButtons;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject levelUpSelectedFirst;

    public bool IsLevelingUp { get; set; }

    public LevelUpSelectionButton[] LevelUpButtons { get => levelUpButtons; set => levelUpButtons = value; }
    public GameObject LevelUpPanel { get => levelUpPanel; set => levelUpPanel = value; }

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's already an instance of UIController");
        }
        Instance = this;
    }


    public void UpdateExperience(int currentExp, int levelExp, int currentLevel) {
        xpSlider.maxValue = levelExp;
        xpSlider.value = currentExp;
        levelText.text = $"Level: {currentLevel}";
    }

    public void SetSelected() {
        EventSystem.current.SetSelectedGameObject(levelUpSelectedFirst);
    }

    public void SkipLevelUp() {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
