using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed;

    private Vector3 moveDirection;
    private Animator anim;

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
