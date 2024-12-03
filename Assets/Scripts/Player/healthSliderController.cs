using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderController : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider; // Reference to the UI Slider

    [SerializeField]
    private float maxHealth = 100f; // Maximum health value

    private float currentHealth; // Current health value

    // Property to get and set the current health
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            UpdateHealthUI();
            if(currentHealth <= 0)
            {
                gameObject.GetComponent<PlayerPhysics>().enabled = false;
                gameObject.GetComponent<PlayerController>().enabled = false;
                gameObject.GetComponent<PlayerAnimations>().enabled = false;
                gameObject.GetComponent<WeaponHandler>().enabled = false;
                
                Debug.Log("Player is dead.");
            }   
        }
    }

    // Initialize health on start
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update the UI Slider based on current health
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth; // Normalized value
        }
        else
        {
            Debug.LogWarning("HealthSlider is not assigned in the Inspector.");
        }
    }

    // Method to damage the player
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current Health: {(CurrentHealth / maxHealth) * 100f}%");
    }

    // Method to heal the player
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        Debug.Log($"Player healed {amount} health. Current Health: {(CurrentHealth / maxHealth) * 100f}%");
    }
}
