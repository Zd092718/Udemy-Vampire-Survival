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
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerStatController playerStatController;
    private List<Weapon> _fullyLeveledWeapons = new List<Weapon>();
    private Vector3 _moveDirection;
    private Animator _anim;

    #region Properties
    public float PickupRange
    {
        get
        {
            return pickupRange;
        }
        set
        {
            pickupRange = value;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }
    public List<Weapon> UnassignedWeapons
    {
        get
        {
            return unassignedWeapons;
        }
        set
        {
            unassignedWeapons = value;
        }
    }
    public List<Weapon> AssignedWeapons
    {
        get
        {
            return assignedWeapons;
        }
        set
        {
            assignedWeapons = value;
        }
    }
    public int MaxWeapons
    {
        get
        {
            return maxWeapons;
        }
        set
        {
            maxWeapons = value;
        }
    }
    public List<Weapon> FullyLeveledWeapons
    {
        get
        {
            return _fullyLeveledWeapons;
        }
        set
        {
            _fullyLeveledWeapons = value;
        }
    }

    #endregion

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already an instance of PlayerController");
        }
        Instance = this;
    }
    private void Start() {
        _anim = GetComponent<Animator>();

        if(assignedWeapons.Count == 0) {
            AddWeapon(Random.Range(0, unassignedWeapons.Count)); 
        }

        moveSpeed = playerStatController.MoveSpeed[0].value;
        pickupRange = playerStatController.PickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(playerStatController.MaxWeapons[0].value);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {

        _moveDirection = gameInput.GetMovementVectorNormalized();
        if (_moveDirection.x > 0)
        {
            sprite.flipX = true;
        } else if (_moveDirection.x < 0)
        {
            sprite.flipX = false;
        }

        transform.position += _moveDirection * (moveSpeed * Time.deltaTime);

        if(_moveDirection != Vector3.zero) {
            _anim.SetBool("IsWalking", true);
        } else {
            _anim.SetBool("IsWalking", false);
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
