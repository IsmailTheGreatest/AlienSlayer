using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [Header("Health Power-Up Settings")]
    [Tooltip("Amount of health to restore when collected.")]
    [SerializeField]
    private float healthRestoreAmount = 50f; // Restores up to 50% of player's health

    [Tooltip("Tag assigned to the player GameObject.")]
    [SerializeField]
    private string playerTag = "Player";

    void Start()
    {
        // The FloatingEffect component handles animations upon starting
        // Ensure that the FloatingEffect script is attached
        if (GetComponent<FloatingEffect>() == null)
        {
            Debug.LogWarning($"{gameObject.name}: FloatingEffect component is not attached. Adding it automatically.");
            gameObject.AddComponent<FloatingEffect>();
        }

        Destroy(gameObject, 5f);

        // Optionally, adjust the linger time if needed
        // Currently, the Destroy is handled within the PowerUpSpawner
    }

    /// <summary>
    /// Detects when another collider enters the trigger attached to this power-up.
    /// </summary>
    /// <param name="collision">Collision data associated with this trigger event.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the specified player tag
        if (collision.CompareTag(playerTag))
        {
            // Attempt to get the HealthSliderController component from the player
            HealthSliderController healthController = collision.GetComponent<HealthSliderController>();
            if (healthController != null)
            {
                // Calculate the heal amount without exceeding the maximum health
                float currentHealth = healthController.CurrentHealth;
                float maxHealth = healthController.MaxHealth;

                float healAmount = Mathf.Min(healthRestoreAmount, maxHealth - currentHealth);
                healthController.Heal(healAmount);

                Debug.Log($"HealthPowerUp collected! Player health increased by {healAmount}%.");

                // Optionally, play a sound or particle effect here

                // Destroy the power-up after collection
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning($"{playerTag} does not have a HealthSliderController component.");
            }
        }
    }
}
