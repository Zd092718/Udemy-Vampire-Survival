using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float speed;
    [SerializeField] private float minSize, maxSize;

    private float activeSize;
    
    void Start()
    {
        activeSize = maxSize;
        speed = speed * Random.Range(.75f, 1.25f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

        if (transform.localScale.x == activeSize) {
            if(activeSize == maxSize) {
                activeSize = minSize;
            } else {
                activeSize = maxSize;   
            }
        }
    }
}
