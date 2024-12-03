using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Reference to the monster prefab
    public float spawnRate = 2.0f; // Time interval between spawns
    public Vector2 spawnAreaMin; // Minimum x and y coordinates for the spawn area
    public Vector2 spawnAreaMax; // Maximum x and y coordinates for the spawn area
    public int maxMonsters = 30; // Maximum number of monsters to spawn
    private int currentMonsters = 0; // Current number of spawned monsters

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    void setMonsterNumber(int number)
    {
        maxMonsters = number;
    }

    // Coroutine to spawn monsters at regular intervals
    IEnumerator SpawnMonsters()
    {
        while (currentMonsters < maxMonsters)
        {
            SpawnMonster();
            currentMonsters++;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Method to spawn a monster at a random position within the spawn area
    void SpawnMonster()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
