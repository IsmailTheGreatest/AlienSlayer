using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Reference to the monster prefab
    public float spawnRate = 2.0f; // Time interval between spawns
    public Vector2 spawnAreaMin; // Minimum x and y coordinates for the spawn area
    public Vector2 spawnAreaMax; // Maximum x and y coordinates for the spawn area

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    // Coroutine to spawn monsters at regular intervals
    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Method to spawn a monster at a random position within the spawn area
    void SpawnMonster()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
