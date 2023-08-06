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

    [Header("Scripts to Deactivated on Pause")]
    [SerializeField] private PlayerController player;

    [Header("First Selected Options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    [SerializeField] private bool isOnMainMenu;

    private bool _isPaused;

    private void Start() {
        if (!isOnMainMenu)
        {
            mainMenuCanvas.SetActive(false);
        }
        settingsCanvas.SetActive(false);
    }

    private void Update() {
        if (GameInput.Instance.MenuOpenCloseInput) {
            if (!UIController.Instance.IsLevelingUp) {
                if (!_isPaused) {
                    Pause();
                } else {
                    Unpause();
                }
            }
        }
    }

    #region Pause/Unpause Functions
    private void Pause() {
        _isPaused = true;
        Time.timeScale = 0f;
        player.enabled = false;

        OpenMainMenu();
    }

    private void Unpause() {
        _isPaused = false;
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
        SceneManager.LoadScene("Menu");
    }

    public void OnStartPress()
    {
        //Start Game
        SceneManager.LoadScene("Main");
    }

    public void OnQuitPress()
    {
        Application.Quit(0);
    }

    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress() {
        OpenMainMenu();
    }

    #endregion
}
