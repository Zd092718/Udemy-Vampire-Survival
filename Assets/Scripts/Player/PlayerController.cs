using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pickupRange = 1.5f;
    [SerializeField] private Weapon activeWeapon;

    private Vector3 moveDirection;
    private Animator anim;

    public float PickupRange { get => pickupRange; set => pickupRange = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Weapon ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already an instance of PlayerController");
        }
        Instance = this;
    }
    private void Start() {
        anim = GetComponent<Animator>();    
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {

        moveDirection = gameInput.GetMovementVectorNormalized();
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if(moveDirection != Vector3.zero) {
            anim.SetBool("IsWalking", true);
        } else {
            anim.SetBool("IsWalking", false);
        }
    }
}
