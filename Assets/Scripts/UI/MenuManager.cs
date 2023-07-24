using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsCanvas;

    [Header("Scripts to Deactive on Pause")]
    [SerializeField] private PlayerController player;

    [Header("First Selected Options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    private bool isPaused;

    private void Start() {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

    private void Update() {
        if (GameInput.Instance.MenuOpenCloseInput) {
            if (!UIController.Instance.IsLevelingUp) {
                if (!isPaused) {
                    Pause();
                } else {
                    Unpause();
                }
            }
        }
    }

    #region Pause/Unpause Functions
    private void Pause() {
        isPaused = true;
        Time.timeScale = 0f;
        player.enabled = false;

        OpenMainMenu();
    }

    private void Unpause() {
        isPaused = false;
        Time.timeScale = 1f;
        player.enabled = true;

        CloseAllMenus();
    }
    #endregion

    #region Activations/Deactivations
    private void OpenMainMenu() {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenuFirst);

    }

    private void CloseAllMenus() {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnSettingsMenuHandled() {
        settingsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }
    #endregion

    #region Main Menu Button Actions
    public void OnSettingsPress() {
        OnSettingsMenuHandled();
    }

    public void OnResumePress() {
        Unpause();
    }

    public void OnMainMenuPress() {
        //Go back to main menu
        //SceneManager.LoadScene("Main");
    }

    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress() {
        OpenMainMenu();
    }

    #endregion
}
