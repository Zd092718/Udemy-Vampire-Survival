using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pickupRange = 1.5f;
    [SerializeField] private int maxWeapons = 3;

    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private List<Weapon> unassignedWeapons, assignedWeapons;
    private List<Weapon> fullyLeveledWeapons = new List<Weapon>();
    private Vector3 moveDirection;
    private Animator anim;

    #region Properties
    public float PickupRange { get => pickupRange; set => pickupRange = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public List<Weapon> UnassignedWeapons { get => unassignedWeapons; set => unassignedWeapons = value; }
    public List<Weapon> AssignedWeapons { get => assignedWeapons; set => assignedWeapons = value; }
    public int MaxWeapons { get => maxWeapons; set => maxWeapons = value; }
    public List<Weapon> FullyLeveledWeapons { get => fullyLeveledWeapons; set => fullyLeveledWeapons = value; }
    #endregion

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already an instance of PlayerController");
        }
        Instance = this;
    }
    private void Start() {
        anim = GetComponent<Animator>();

        if(assignedWeapons.Count == 0) {
        AddWeapon(Random.Range(0, unassignedWeapons.Count)); 
        }
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

    public void AddWeapon(int weaponNumber) {
        if(weaponNumber < unassignedWeapons.Count) {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd) {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
