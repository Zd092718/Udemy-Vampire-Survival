using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }  

    public bool MenuOpenCloseInput { get; private set; }

    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private InputAction menuOpenCloseAction;

    private void Awake() {

        if(Instance == null) {
            Instance = this;
        }
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerInput = GetComponent<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }

    private void Update() {
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = playerControls.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

}
