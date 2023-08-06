using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float currentHealth, maxHealth;
    [Header("References")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private PlayerStatController statController;

    #region Properties
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    #endregion

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already a HealthController in the scene");
        }
        Instance = this;
    }

    void Start()
    {
        maxHealth = statController.Health[0].value;

        currentHealth = maxHealth;

        healthBarImage.fillAmount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            TakeDamage(5);
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if(currentHealth <= 0 ) {
            //Die
            gameObject.SetActive(false);
        }
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        DamageNumberController.Instance.SpawnDamage(damage, transform.position, Color.red);
    }
}
