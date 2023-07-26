using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }  

    public bool MenuOpenCloseInput { get; private set; }

    private PlayerControls _playerControls;
    private PlayerInput _playerInput;
    private InputAction _menuOpenCloseAction;

    private void Awake() {

        if(Instance == null) {
            Instance = this;
        }
        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();

        _playerInput = GetComponent<PlayerInput>();
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void Update() {
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

}
