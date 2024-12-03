using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [Tooltip("List of power-up prefabs to spawn.")]
    [SerializeField]
    private List<GameObject> powerUpPrefabs;

    [Header("Spawn Area Settings")]
    [Tooltip("Minimum X and Y coordinates for spawn area.")]
    [SerializeField]
    private Vector2 spawnAreaMin;

    [Tooltip("Maximum X and Y coordinates for spawn area.")]
    [SerializeField]
    private Vector2 spawnAreaMax;

    [Header("Spawn Timing")]
    [Tooltip("Time interval between spawns in seconds.")]
    [SerializeField]
    private float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            SpawnRandomPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomPowerUp()
    {
        if (powerUpPrefabs.Count == 0)
        {
            Debug.LogWarning("No power-up prefabs assigned to the PowerUpSpawner.");
            return;
        }

        // Select a random power-up prefab
        int prefabIndex = Random.Range(0, powerUpPrefabs.Count);
        GameObject powerUpPrefab = powerUpPrefabs[prefabIndex];

        // Generate a random position within the specified range
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // Instantiate the power-up prefab at the random position
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}
