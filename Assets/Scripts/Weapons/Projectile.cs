using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    #region Properties
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
    #endregion

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (MoveSpeed * Time.deltaTime);
    }
}
