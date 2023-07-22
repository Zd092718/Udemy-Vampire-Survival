using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private LevelUpSelectionButton[] levelUpButtons;
    [SerializeField] private GameObject levelUpPanel;

    public LevelUpSelectionButton[] LevelUpButtons { get => levelUpButtons; set => levelUpButtons = value; }
    public GameObject LevelUpPanel { get => levelUpPanel; set => levelUpPanel = value; }

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's already an instance of UIController");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLevel) {
        xpSlider.maxValue = levelExp;
        xpSlider.value = currentExp;
        levelText.text = $"Level: {currentLevel}";
    }
}
