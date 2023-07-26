using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// ReSharper disable All

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("Level Settings & References")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private LevelUpSelectionButton[] levelUpButtons;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject levelUpSelectedFirst;


    #region Properties

    public bool IsLevelingUp { get; set; }

    public LevelUpSelectionButton[] LevelUpButtons
    {
        get
        {
            return levelUpButtons;
        }
        set
        {
            levelUpButtons = value;
        }
    }
    public GameObject LevelUpPanel
    {
        get
        {
            return levelUpPanel;
        }
        set
        {
            levelUpPanel = value;
        }
    }

    #endregion

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
        IsLevelingUp = false;
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
