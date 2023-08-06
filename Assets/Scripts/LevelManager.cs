using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake() {
        Instance = this;
    }

    private bool _gameActive;
    private float _timer;


    #region Properties
    public bool GameActive { get => _gameActive; set => _gameActive = value; }
    public float Timer { get => _timer; set => _timer = value; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameActive) {
            _timer += Time.deltaTime;
            UIController.Instance.UpdateTimer(_timer);
        }
    }

    public void EndLevel() {
        _gameActive = false;
        UIController.Instance.DisplayGameOverScreen();
    }
}
