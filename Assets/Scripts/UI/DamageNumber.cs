using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageNumberText;
    [SerializeField] private float lifeTime;
    [SerializeField] private float floatSpeed = 1f;
    private float lifeCounter;


    // Update is called once per frame
    void Update()
    {
        if(lifeCounter > 0) {
            lifeCounter -= Time.deltaTime;

            if(lifeCounter <= 0 ) {
                DamageNumberController.Instance.PlaceInPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int damageDisplay) {
        lifeCounter = lifeTime;

        damageNumberText.text = damageDisplay.ToString();
    }

    public TMP_Text GetDamageNumberText() {
        return damageNumberText;
    }

}
