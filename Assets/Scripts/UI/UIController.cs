using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
// ReSharper disable All

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private LevelUpSelectionButton[] levelUpButtons;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject levelUpSelectedFirst;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private PlayerStatController playerStatController;
    [SerializeField] private PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameOverSelectedFirst;
    [SerializeField] private TMP_Text endTimeText;


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

    public PlayerStatUpgradeDisplay MoveSpeedUpgradeDisplay { get => moveSpeedUpgradeDisplay; set => moveSpeedUpgradeDisplay = value; }
    public PlayerStatUpgradeDisplay HealthUpgradeDisplay { get => healthUpgradeDisplay; set => healthUpgradeDisplay = value; }
    public PlayerStatUpgradeDisplay PickupRangeUpgradeDisplay { get => pickupRangeUpgradeDisplay; set => pickupRangeUpgradeDisplay = value; }
    public PlayerStatUpgradeDisplay MaxWeaponsUpgradeDisplay { get => maxWeaponsUpgradeDisplay; set => maxWeaponsUpgradeDisplay = value; }

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

    public void UpdateCoins(int currentCoins) {
        coinText.text = $"Coins: {currentCoins}";
    }

    public void UpdateTimer(float time) {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = minutes + ":" + seconds.ToString("00");
        endTimeText.text = minutes + ":" + seconds.ToString("00");
    }

    public void SetLevelUpSelected() {
        EventSystem.current.SetSelectedGameObject(levelUpSelectedFirst);
    }

    public void SetGameOverSelected() {
        EventSystem.current.SetSelectedGameObject(gameOverSelectedFirst);
    }

    public void SkipLevelUp() {
        IsLevelingUp = false;
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PurchaseMoveSpeed() {
        playerStatController.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth() {
        playerStatController.PurchaseHealth();
        SkipLevelUp() ;
    }

    public void PurchasePickupRange() {
        playerStatController.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons() {
        playerStatController.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void DisplayGameOverScreen() {
        StartCoroutine(GameOverDisplayWaitRoutine());
    }

    IEnumerator GameOverDisplayWaitRoutine() {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
