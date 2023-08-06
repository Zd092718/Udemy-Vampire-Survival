using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsCanvas;

    [Header("First Selected Options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    private void Start() {
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
    #region Main Menu Button Actions
    public void OnSettingsPress() {
        OnSettingsMenuHandled();
    }

    public void OnStartPress() {
        //Start Game
        SceneManager.LoadScene("Main");
    }

    public void OnQuitPress() {
        Application.Quit(0);
    }

    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress() {
        mainMenuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    #endregion
}
